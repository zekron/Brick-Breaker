using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/PersistentData", fileName = "NewPersistentData")]
public class PersistentDataSO : ScriptableObject
{
    private SortedDictionary<string, int> topScoreBoard = new SortedDictionary<string, int>();

    public string CurrentPlayerName;
    public int CurrentPlayerScore;

    public SortedDictionary<string, int> TopScoreBoard { get => topScoreBoard; }

    public void AddTopScoreBoard(string name, int score)
    {
        topScoreBoard.Add(name, score);
    }

    public int GetTopScoreBoardInRank(int rank)
    {
        int cnt = 0;
        foreach (var item in topScoreBoard)
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
