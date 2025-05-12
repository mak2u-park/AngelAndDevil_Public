using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


public class ScoreManager : Singleton<ScoreManager> { 
{
    private ScoreData scoredata;
    private string json = ".json";
    private int angelhostage;
    private int devilhostage;

    
    public int AngelHostage
    {
        get { return angelhostage;}
    }

    public int DevilHostage
    {
        get { return devilhostage;}
    }

    private void Start()
    {
        
    }

    public void SettingHostage(int angel, int devil) // 처음에 인질 개수를 세팅
    {
        angelhostage = GameManager.Instance.getHostageCount(HostageType.Knight);
        devilhostage = GameManager.Instance.getHostageCount(HostageType.DarkMage);
    }

    public void SettingTime()
    {

    }

    public void RemoveHostage(HostageType type)
    {
        if(type == HostageType.Knight)
        {
            angelhostage--;
        }
        else
        {
            devilhostage--;
        }
    }

    private int EndStageScore(int stage)
    {
        int newscore = 1 + CheckHostage() + CheckTime();
        if (GameManager.Instance.GetStageScore(stage) < newscore)
        {
            GameManager.Instance.SaveStageScore(stage, newscore);
        }
        return newscore;
    }

    private int CheckHostage()
    {
        int lefthostage = angelhostage + devilhostage;
        if(lefthostage >= 0)
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
}
