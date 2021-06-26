using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/PersistentData", fileName = "NewPersistentData")]
public class PersistentDataSO : ScriptableObject
{
    public SortedDictionary<string, int> TopScoreBoard;

    public string CurrentPlayerName;
    public int CurrentPlayerScore;
}
