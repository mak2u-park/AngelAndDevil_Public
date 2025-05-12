using UnityEngine;
using System;
using System.IO;

public class GameManager : Singleton<GameManager>
{
    private GameData gameData;
    private string json = ".json";
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

    public void SaveStageScore(int stage, int score)
    {
        gameData.SetScore(stage, score);
    }

    public int GetStageScore(int stage)
    {
        return gameData.GetScore(stage);
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

    private void SaveScore(int slot)
    {
        string scordatatojson = JsonUtility.ToJson(gameData);
        string Filpath = Path.Combine(Application.dataPath + "/Data/", slot.ToString() + "slot" + json);
        File.WriteAllText(Filpath, scordatatojson);
        Debug.Log("저장 완료");
    }
}
