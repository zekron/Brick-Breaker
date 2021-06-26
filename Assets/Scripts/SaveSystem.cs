using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private PersistentDataSO PersistentData;

    private static SaveSystem _instance = default;
    public static SaveSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveSystem>();
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddScoreBoard(string name, int score)
    {
        PersistentData.TopScoreBoard.Add(name, score);
    }
    public IDictionary<string, int> GetScoreBoard()
    {
        return PersistentData.TopScoreBoard;
    }

    public void SetCurrentPlayer(string name, int score)
    {
        SetCurrentPlayerName(name);
        SetCurrentPlayerScore(score);
    }
    public void SetCurrentPlayerName(string name)
    {
        PersistentData.CurrentPlayerName = name;
    }
    public void SetCurrentPlayerScore(int score)
    {
        PersistentData.CurrentPlayerScore = score;
    }
}
