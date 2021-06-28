using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/PersistentData", fileName = "NewPersistentData")]
public class PersistentDataSO : ScriptableObject
{
    private SortedDictionary<int, List<string>> topScoreBoard = new SortedDictionary<int, List<string>>();
    public string CurrentPlayerName = "New Player";
    public int CurrentPlayerScore = 0;

    public SortedDictionary<int, List<string>> TopScoreBoard { get => topScoreBoard; }

    public void AddRecord(string name, int score)
    {
        if (!topScoreBoard.ContainsKey(score))
            topScoreBoard.Add(score, new List<string>());

        topScoreBoard[score].Add(name);
    }

    public int GetRecordScoreInRank(int rank)
    {
        int cnt = topScoreBoard.Count - 1;
        foreach (var item in topScoreBoard)
        {
            if (cnt > rank)
            {
                cnt--;
                continue;
            }
            else
                return item.Key;
        }
        return 0;
    }
    public string GetRecordNameInRank(int rank)
    {
        int cnt = topScoreBoard.Count - 1;
        foreach (var item in topScoreBoard)
        {
            if (cnt > rank)
            {
                cnt--;
                continue;
            }
            else
                return item.Value[0];
        }
        return "NULL";
    }
}
