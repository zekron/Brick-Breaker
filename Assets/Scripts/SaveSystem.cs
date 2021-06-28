using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private PersistentDataSO PersistentData;

    private const string FILE_NAME = "Brick.Breaker";
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
        string json = JsonConvert.SerializeObject(PersistentData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, FILE_NAME), json);
    }

    private void LoadFile()
    {
        string path = Path.Combine(Application.persistentDataPath, FILE_NAME);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PersistentDataSO data = JsonConvert.DeserializeObject<PersistentDataSO>(json);
            PersistentData = data;
        }
    }

    public void AddRecord(string name, int score)
    {
        PersistentData.AddRecord(name, score);
        SaveFile();
    }

    public Tuple<int, string> GetRecordInRank(int rank)
    {
        return new Tuple<int, string>(GetRecordScoreInRank(rank), GetRecordNameInRank(rank));
    }
    public int GetRecordScoreInRank(int rank)
    {
        return PersistentData.GetRecordScoreInRank(rank);
    }
    public string GetRecordNameInRank(int rank)
    {
        return PersistentData.GetRecordNameInRank(rank);
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
