using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private PersistentDataSO PersistentData;

    private const string FILE_NAME = "";
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

        LoadFile();
    }

    private void SaveFile()
    {
        //TODO
        string json = JsonUtility.ToJson(PersistentData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, FILE_NAME), json);
    }

    private void LoadFile()
    {
        string path = Path.Combine(Application.persistentDataPath, FILE_NAME);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PersistentDataSO data = JsonUtility.FromJson<PersistentDataSO>(json);

            PersistentData.LoadRecords(data.TopScoreBoard);
        }
    }

    public void AddRecord(string name, int score)
    {
        PersistentData.AddRecord(name, score);
        SaveFile();
    }

    public int GetRecordInRank(int rank)
    {
        return PersistentData.GetRecordInRank(rank);
    }
    public int GetRrcordCount()
    {
        return PersistentData.TopScoreBoard.Count;
    }
    public List<Tuple<int, string>> GetFullRecords()
    {
        List<Tuple<int, string>> records = new List<Tuple<int, string>>(ScoreBoard.MAX_RECORD_COUNT);

        foreach (var record in PersistentData.TopScoreBoard)
        {
            foreach (var name in record.Value)
            {
                records.Add(new Tuple<int, string>(record.Key, name));
            }
        }
        return records;
    }

    public void SetCurrentPlayer(string name, int score)
    {
        SetCurrentPlayerName(name);
        SetCurrentPlayerScore(score);
    }
    public Tuple<string, int> GetCurrentPlayer()
    {
        return new Tuple<string, int>(GetCurrentPlayerName(), GetCurrentPlayerScore());
    }
    public void SetCurrentPlayerName(string name)
    {
        PersistentData.CurrentPlayerName = name;
    }
    public string GetCurrentPlayerName()
    {
        return PersistentData.CurrentPlayerName;
    }
    public void SetCurrentPlayerScore(int score)
    {
        PersistentData.CurrentPlayerScore = score;
    }
    public int GetCurrentPlayerScore()
    {
        return PersistentData.CurrentPlayerScore;
    }
}
