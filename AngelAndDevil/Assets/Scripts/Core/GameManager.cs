using UnityEngine;
using System;
using System.IO;
using UnityEditor.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameData gameData;
    private string slotnumberkey = "slotnumber";
    private string json = ".json";
    private float _time = 0;
    private int _stage = 0;
    private int choicedSlot;

    public int tema;//나중에 프로퍼티 넣어주기
    public bool isClear;
    public bool isGameOver;
    public Vector3[] AgDvPosition =
    {
        new Vector3(-6, -1, 0),
        new Vector3(-6, -1, 0),
        new Vector3(-6, -1, 0),
        new Vector3(-6, -1, 0)
    };

    public int ChoicedSlot
    {
        set {choicedSlot = value;} 
        get { return choicedSlot; }
    }

    public string Slotnumberkey
    {
        get { return slotnumberkey; }
    }

    public float _Time
    {
        get { return _time; }
    }

    public int _Stage
    {
        get { return _stage; }
    }


    public int getHostageCount(HostageType type)
    {
        Hostage[] hostages = FindObjectsOfType<Hostage>();
        int count = 0;
        foreach(Hostage hostage in hostages)
        {
            if(hostage.Type == type)
            {
                count++;
            }
        }
        return count;
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        gameData= new GameData();
        Debug.Log("게임데이터 새로 생성");
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    public void StartGame(int stage)
    {
        isClear = false;
        isGameOver = false;
        _time = 0f;
        _stage = stage;
        Time.timeScale = 1.0f;
        ScoreManager.Instance.SettingHostage();
        SoundManager.Instance.PlayBGM("MainMusic");
        Debug.Log("현재 스테이지 : " + _stage);
    }

    public void Pause(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Clear()
    {
        ScoreManager.Instance.EndStageScore();
        isClear = true;
        Debug.Log("clear 호출");
    }
    
    public void InvokeClear()
    {
        Invoke("Clear", 1.5f);
    }

    public void IsGameOver()
    {
        isGameOver = true;
    }

    public void invokeGameOver()
    {
        Invoke("IsGameOver", 1.5f);
    }

    public void SaveStageScore(int stage, int score)
    {
        gameData.SetScore(stage, score);
    }

    public int GetStageScore(int stage)//스테이지 클리어 시 점수
    {
        return gameData.GetScore(stage);
    }

    public void SaveStageLeftAngel(int stage, int leftangel)
    {
        gameData.SetLeftAngel(stage, leftangel);
    }

    public int GetStageLeftAngel(int stage)//스테이지 클리어 시 남은 천사 수
    {
        return gameData.GetLeftAngel(stage);
    }

    public void SaveStageLeftDevil(int stage, int leftdevil)
    {
        gameData.SetLeftDevil(stage, leftdevil);
    }

    public int GetStageLeftDevil(int stage)//스테이지 클리어 시 남은 악마 수
    {
        return gameData.GetLeftDevil(stage);
    }

    public void SaveStageIntime(int stage, bool intime)
    {
        gameData.SetIntime(stage, intime);
    }

    public bool GetStageIntime(int stage)//시간 안에 스테이지 클리어 여부
    {
        return gameData.GetIntime(stage);
    }

    public bool TrySelectStage(int stage)//스테이지 출입 가능 여부
    {
        return gameData.CanSelectStage(stage);
    }

    public void SaveMaxStage(int stage)
    {
        gameData.MaxStage = stage;
    }

    public int GetMaxStage()
    {
        return gameData.MaxStage;
    }


    public bool ShowOutcome(ref EndingType ending)
    {
        if(gameData.GetScore(12) == 0)
        {
            return false;
        }
        int leftangel = 0;
        int leftdevil = 0;
        for(int i = 1; i <= 12; i++)
        {
            leftangel += gameData.GetLeftAngel(i);
            leftdevil += gameData.GetLeftDevil(i);
        }
        if(leftangel == 0 && leftdevil == 0)
        {
            ending = EndingType.perfect;
        }
        else if(leftangel == 0)
        {
            ending = EndingType.angel;
        }
        else if(leftdevil == 0)
        {
            ending = EndingType.devil;
        }
        else
        {
            ending = EndingType.normal;
        }
        return true;
    }

    public bool LoadData(int slot)
    {
        string Filepath = Path.Combine(Application.dataPath + "/Data/", slot.ToString() + "slot" + json);
        if (File.Exists(Filepath))
        {
            Debug.Log("파일 불러오기");
            string loadedData = File.ReadAllText(Filepath);
            gameData = JsonUtility.FromJson<GameData>(loadedData);
            Debug.Log($"MaxStage : {gameData.MaxStage}");
            return true;
        }
        Debug.Log($"{slot}번 파일이 없음");
        return false;
    }

    public void SaveData(int slot) // 덮어 씌우기
    {
        if(gameData == null)
        {
            Debug.Log("게임데이터가 null임");
            return;
        }
        string scordatatojson = JsonUtility.ToJson(gameData);
        string Filpath = Path.Combine(Application.dataPath + "/Data/", slot.ToString() + "slot" + json);
        File.WriteAllText(Filpath, scordatatojson);
        Debug.Log($"{slot}번 저장 완료");
    }

    public void SaveDataOnNewSlot()
    {
        if(PlayerPrefs.HasKey(slotnumberkey))
        {
            int slot = PlayerPrefs.GetInt(slotnumberkey);
            SaveData(slot);
            PlayerPrefs.SetInt(slotnumberkey, slot + 1);
        }
        else
        {
            SaveData(0);
            PlayerPrefs.SetInt(slotnumberkey, 1);
        }
    }
}
