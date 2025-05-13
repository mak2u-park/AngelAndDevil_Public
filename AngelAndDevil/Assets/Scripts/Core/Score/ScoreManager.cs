using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


public class ScoreManager : Singleton<ScoreManager> 
{
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

    public void SettingHostage() // 처음에 인질 개수를 세팅
    {
        angelhostage = GameManager.Instance.getHostageCount(HostageType.Knight);
        devilhostage = GameManager.Instance.getHostageCount(HostageType.DarkMage);
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

    private void EndStageScore(int stage)//이거 어디서 호출해주나요?
    {
        int newscore = 1 + CheckHostage() + CheckTime();
        if (GameManager.Instance.GetStageScore(GameManager.Instance._Stage) > newscore)
        {
            GameManager.Instance.SaveStageScore(GameManager.Instance._Stage, newscore);
        }
        if(GameManager.Instance.GetStageLeftAngel(GameManager.Instance._Stage) > angelhostage)
        {
            GameManager.Instance.SaveStageLeftAngel(GameManager.Instance._Stage, angelhostage);
        }
        if(GameManager.Instance.GetStageLeftDevil(GameManager.Instance._Stage) > devilhostage)
        {
            GameManager.Instance.SaveStageLeftDevil(GameManager.Instance._Stage, devilhostage);
        }
        if(CheckTime() == 1)
        {
            GameManager.Instance.SaveStageIntime(GameManager.Instance._Stage, true);
        }
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
        float checktime = GameManager.Instance._Time; // 남은 time을 구함
        if (checktime > SettingData.limitTime[GameManager.Instance._Stage - 1])
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
