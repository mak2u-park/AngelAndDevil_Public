using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScoreData
{
    public int[] scores;

    public ScoreData()
    {
        scores = new int[5];
        for(int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }
    }
}

public class ScoreManager : MonoBehaviour
{
    private ScoreData scoredata;
    protected static ScoreManager instance;
    private string json = ".json";

    public static ScoreManager Instance
    {
        get { return instance; }
    }

    public int StageScore(int stage)
    {
        return scoredata.scores[stage];
    }

    private void Start()
    {
        instance = this;
        LoadScore();
        DontDestroyOnLoad(gameObject);
    }

    private int EndStageScore(int stage)
    {
        int newscore = 1 + CheckHostage() + CheckTime();
        if (scoredata.scores[stage] < newscore)
        {
            scoredata.scores[stage] = newscore;
        }
        return newscore;
    }

    private int CheckHostage()
    {
        int hostagenum = 1; // 남은 인질들의 개수를 구함
        if(hostagenum > 0)
        {
            return 0;
        }
        return 1;
    }

    private int CheckTime()
    {
        float time = 1.0f; // 남은 time을 구함
        if(time == 0)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    private void LoadScore()
    {
        string Filepath = Application.persistentDataPath + json;
        if(File.Exists(Filepath))
        {
            Debug.Log("파일 불러오기");
            string loadedData = File.ReadAllText(Filepath);
            scoredata = JsonUtility.FromJson<ScoreData>(loadedData);
            return;
        }
        Debug.Log("파일이 없음, 새로 파일 생성...");
        scoredata = new ScoreData();
    }

    private void SaveScore()
    {
        string scordatatojson = JsonUtility.ToJson(scoredata);
        string Filpath = Application.persistentDataPath + json;
        File.WriteAllText(Filpath, scordatatojson);
        Debug.Log("저장 완료");
    }
}
