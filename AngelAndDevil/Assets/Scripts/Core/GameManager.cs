using UnityEngine;
using System;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEditor.Search;

public class GameManager : Singleton<GameManager>
{
    private GameData gameData;
    private string json = ".json";
    private float _time = 0;
    private int _stage = 0;

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
        gameData= new GameData();
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    public void StartGame(int stage)
    {
        _time = 0f;
        _stage = stage;
        ScoreManager.Instance.SettingHostage();
    }

    public void SaveStageScore(int stage, int score)
    {
        gameData.SetScore(stage, score);
    }

    public int GetStageScore(int stage)
    {
        return gameData.GetScore(stage);
    }

    public void SaveStageLeftAngel(int stage, int leftangel)
    {
        gameData.SetLeftAngel(stage, leftangel);
    }

    public int GetStageLeftAngel(int stage)
    {
        return gameData.GetLeftAngel(stage);
    }

    public void SaveStageLeftDevil(int stage, int leftdevil)
    {
        gameData.SetLeftDevil(stage, leftdevil);
    }

    public int GetStageLeftDevil(int stage)
    {
        return gameData.GetLeftDevil(stage);
    }

    public void SaveStageIntime(int stage, bool intime)
    {
        gameData.SetIntime(stage, intime);
    }

    public bool GetStageIntime(int stage)
    {
        return gameData.GetIntime(stage);
    }

    public bool TrySelectStage(int stage)
    {
        return gameData.CanSelectStage(stage);
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

    private void LoadData(int slot)
    {
        string Filepath = Path.Combine(Application.dataPath + "/Data/", slot.ToString() + "slot" + json);
        if (File.Exists(Filepath))
        {
            Debug.Log("파일 불러오기");
            string loadedData = File.ReadAllText(Filepath);
            gameData = JsonUtility.FromJson<GameData>(loadedData);
            return;
        }
        Debug.Log("파일이 없음, 새로 파일 생성...");
        gameData = new GameData();
    }

    private void SaveData(int slot)
    {
        string scordatatojson = JsonUtility.ToJson(gameData);
        string Filpath = Path.Combine(Application.dataPath + "/Data/", slot.ToString() + "slot" + json);
        File.WriteAllText(Filpath, scordatatojson);
        Debug.Log("저장 완료");
    }
}
