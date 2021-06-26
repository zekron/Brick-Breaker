using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/PersistentData", fileName = "NewPersistentData")]
public class PersistentDataSO : ScriptableObject
{
    public SortedDictionary<string, int> TopScoreBoard { get; }

    public string CurrentPlayerName;
    public int CurrentPlayerScore;

    public void AddTopScoreBoard(string name, int score)
    {
        TopScoreBoard.Add(name, score);
    }

    public int GetTopScoreBoardInRank(int rank)
    {
        int cnt = 0;
        foreach (var item in TopScoreBoard)
        {
            if (cnt < rank)
            {
                cnt++;
                continue;
            }
            else
                return item.Value;
        }
        return -1;
    }
}
