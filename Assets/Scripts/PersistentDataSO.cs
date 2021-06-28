using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/PersistentData", fileName = "NewPersistentData")]
public class PersistentDataSO : ScriptableObject
{
    private SortedDictionary<int, List<string>> topScoreBoard = new SortedDictionary<int, List<string>>();

    public string CurrentPlayerName;
    public int CurrentPlayerScore;

    public SortedDictionary<int, List<string>> TopScoreBoard { get => topScoreBoard; }

    public void LoadRecords(SortedDictionary<int, List<string>> records)
    {
        topScoreBoard = records;
    }

    public void AddRecord(string name, int score)
    {
        if (!topScoreBoard.ContainsKey(score))
            topScoreBoard.Add(score, new List<string>());

        topScoreBoard[score].Add(name);
    }

    public int GetRecordInRank(int rank)
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
                return item.Key;
        }
        return -1;
    }
}
