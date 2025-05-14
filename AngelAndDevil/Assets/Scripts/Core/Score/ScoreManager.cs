using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


public class ScoreManager : Singleton<ScoreManager> 
{
    private int angelhostage = 0;
    private int devilhostage = 0;

    
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

    public void IncreaseOneAngelHostage()
    {
        angelhostage++;
    }

    public void IncreaseOneDevilHostage()
    {
        devilhostage++;
    }

    public void ResetHostage()
    {
        angelhostage = 0;
        devilhostage = 0;

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

    public void EndStageScore()
    {
        int newscore = 1 + CheckHostage() + CheckTime();
        if (GameManager.Instance.GetStageScore(GameManager.Instance._Stage) <= newscore)
        {
            GameManager.Instance.SaveStageScore(GameManager.Instance._Stage, newscore);
        }
        if(GameManager.Instance.GetStageLeftAngel(GameManager.Instance._Stage) >= angelhostage)
        {
            GameManager.Instance.SaveStageLeftAngel(GameManager.Instance._Stage, angelhostage);
        }
        if(GameManager.Instance.GetStageLeftDevil(GameManager.Instance._Stage) >= devilhostage)
        {
            GameManager.Instance.SaveStageLeftDevil(GameManager.Instance._Stage, devilhostage);
        }
        if(CheckTime() == 1)
        {   
            GameManager.Instance.SaveStageIntime(GameManager.Instance._Stage, true);
        }
        if(GameManager.Instance.GetMaxStage() <= GameManager.Instance._Stage)
        {
            GameManager.Instance.SaveMaxStage(GameManager.Instance._Stage);
            Debug.Log("MaxStage 갱신");
        }
    }

    private int CheckHostage()
    {
        int lefthostage = angelhostage + devilhostage;
        Debug.Log("LeftHostage" + lefthostage);
        if(lefthostage > 0)
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
