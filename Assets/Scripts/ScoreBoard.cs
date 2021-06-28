using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private GameObject _rankGO;
    [SerializeField] private GameObject _nameGO;
    [SerializeField] private GameObject _scoreGO;
    [SerializeField] private GameObject _inputFieldGO;
    [SerializeField] private InputField _inputFieldPlayerName;
    [SerializeField] private GameObject _buttonRestartGO;

    private Text[] _rankTexts;
    private Text[] _nameTexts;
    private Text[] _scoreTexts;
    // Start is called before the first frame update

    public const int MAX_RECORD_COUNT = 10;
    void Start()
    {
        _rankTexts = _rankGO.GetComponentsInChildren<Text>();
        _nameTexts = _nameGO.GetComponentsInChildren<Text>();
        _scoreTexts = _scoreGO.GetComponentsInChildren<Text>();

        for (int i = 0; i < _rankTexts.Length; i++)
        {
            _rankTexts[i].gameObject.SetActive(false);
            _rankTexts[i].color = Color.white;
            _nameTexts[i].gameObject.SetActive(false);
            _nameTexts[i].color = Color.white;
            _scoreTexts[i].gameObject.SetActive(false);
            _scoreTexts[i].color = Color.white;
        }

        _inputFieldPlayerName.text = SaveSystem.Instance.GetCurrentPlayerName();

        _inputFieldGO.SetActive(true);
    }

    void OnDestroy()
    {
        //for (int i = 0; i < _rankTexts.Length; i++)
        //{
        //    _rankTexts[i].gameObject.SetActive(false);
        //    _rankTexts[i].color = Color.white;
        //    _nameTexts[i].gameObject.SetActive(false);
        //    _nameTexts[i].color = Color.white;
        //    _scoreTexts[i].gameObject.SetActive(false);
        //    _scoreTexts[i].color = Color.white;
        //}
    }

    public void SetCurrentPlayerName()
    {
        SaveSystem.Instance.SetCurrentPlayerName(_inputFieldPlayerName.text);
        StartCoroutine(YieldInit());
    }

    public void RestartGame()
    {
        SceneLoader.LoadScene("main");
    }

    private IEnumerator YieldInit()
    {
        Tuple<string, int> temp = SaveSystem.Instance.GetCurrentPlayer();
        SaveSystem.Instance.AddRecord(temp.Item1, temp.Item2);

        yield return new WaitForSeconds(2);

        InitScoreBoardText();
        _inputFieldGO.SetActive(false);
        _buttonRestartGO.SetActive(true);
    }

    private void InitScoreBoardText()
    {
        List<Tuple<int, string>> records = SaveSystem.Instance.GetFullRecords();

        int dCount = records.Count;
        for (int i = 0; i < dCount; i++)
        {
            _rankTexts[dCount - 1 - i].gameObject.SetActive(true);

            _nameTexts[dCount - 1 - i].text = records[i].Item2;
            _nameTexts[dCount - 1 - i].gameObject.SetActive(true);

            _scoreTexts[dCount - 1 - i].text = records[i].Item1.ToString();
            _scoreTexts[dCount - 1 - i].gameObject.SetActive(true);

            if (records[i].Item2 == SaveSystem.Instance.GetCurrentPlayerName())
            {
                _rankTexts[dCount - 1 - i].color = Color.green;
                _nameTexts[dCount - 1 - i].color = Color.green;
                _scoreTexts[dCount - 1 - i].color = Color.green;
            }
        }
    }
}
