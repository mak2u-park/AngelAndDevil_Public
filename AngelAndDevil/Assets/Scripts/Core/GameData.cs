using System.Runtime.CompilerServices;
using UnityEditor.Build.Player;
using UnityEngine;
public class SettingData
{
    public static float[] limitTime = { 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f };
}

public enum EndingType
{
    normal,
    devil,
    angel,
    perfect
}

[System.Serializable]
public class Data
{
    [SerializeField] private int score;
    [SerializeField] private int leftAngel;
    [SerializeField] private int leftDevil;
    [SerializeField] private bool intime;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public int LeftAngel
    {
        get { return leftAngel; }
        set {  leftAngel = value; }
    }
    public int LeftDevil
    {
        get { return leftDevil; }
        set { leftDevil = value; }
    }
    public bool Intime
    {
        get { return  intime; }
        set { intime = value; }
    }

    public Data()
    {
        score = 0;
        leftAngel = 3;
        leftDevil = 3;
        intime = false;
    }
}

[System.Serializable]
public class GameData
{
    [SerializeField] private Data[] data;
    [SerializeField] private int maxStage;
    public Vector3[] LastPosition =
    {
        new Vector3(-6, -1, 0),
        new Vector3(-6, -1, 0),
        new Vector3(-6, -1, 0),
        new Vector3(-6, -1, 0)
    };

    public GameData()
    {
        data = new Data[12];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new Data();
        }
        MaxStage = 0;
        
    }
    public int MaxStage
    {
        get { return maxStage; }
        set { maxStage = value; }
    }

    public void SetScore(int num, int _score)
    {
        data[num-1].Score = _score;
    }

    public int GetScore(int num)
    {
        return data[num - 1].Score;
    }

    public void SetLeftAngel(int num, int _leftangel)
    {
        data[num-1].LeftAngel = _leftangel;
    }
    public int GetLeftAngel(int num)
    {
        return data[num - 1].LeftAngel;
    }
    public void SetLeftDevil(int num, int _leftdevil)
    {
        data[num-1].LeftDevil = _leftdevil;
    }
    public int GetLeftDevil(int num)
    {
        return data[num - 1].LeftDevil;
    }

    public void SetIntime(int num, bool _intime)
    {
        data[num - 1].Intime = _intime;
    }

    public bool GetIntime(int num)
    {
        return data[num - 1].Intime;
    }
    public bool CanSelectStage(int stage)
    {
        int index = stage - 1;
        if(index == 0)
        {
            return true;
        }
        if (data[index-1].Score == 0)
        {
            return false;
        }
        return true;
    }
}
