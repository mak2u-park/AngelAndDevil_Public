using UnityEditor.Build.Player;
using UnityEngine;

public class GameData
{
    private int[] scores;

    public GameData()
    {
        scores = new int[5];
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }
    }
    public void SetScore(int num, int _score)
    {
        scores[num - 1] = _score;
    }

    public int GetScore(int num)
    {
        return scores[num - 1];
    }
}
