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

    private Text[] _rankTexts;
    private Text[] _nameTexts;
    private Text[] _scoreTexts;
    // Start is called before the first frame update
    void Start()
    {
        _rankTexts = _rankGO.GetComponentsInChildren<Text>();
        _nameTexts = _nameGO.GetComponentsInChildren<Text>();
        _scoreTexts = _scoreGO.GetComponentsInChildren<Text>();

        //DontDestroyOnLoad(gameObject);
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
        //_inputFieldGO.SetActive(true);
    }

    public void SetCurrentPlayerName()
    {
        SaveSystem.Instance.SetCurrentPlayerName(_inputFieldPlayerName.text);
        StartCoroutine(YieldInit());
    }

    private IEnumerator YieldInit()
    {
        Tuple<string, int> temp = SaveSystem.Instance.GetCurrentPlayer();
        SaveSystem.Instance.AddScoreBoard(temp.Item1, temp.Item2);

        yield return new WaitForSeconds(2);

        InitScoreBoardText();
        _inputFieldGO.SetActive(false);
    }

    private void InitScoreBoardText()
    {
        IDictionary<string, int> dict = SaveSystem.Instance.GetScoreBoard();
        string[] names = new string[dict.Keys.Count];
        int[] scores = new int[dict.Values.Count];

        dict.Keys.CopyTo(names, 0);
        dict.Values.CopyTo(scores, 0);
        for (int i = 0; i < dict.Count; i++)
        {
            _nameTexts[i].text = names[i];
            _nameTexts[i].gameObject.SetActive(true);

            _scoreTexts[i].text = scores[i].ToString();
            _scoreTexts[i].gameObject.SetActive(true);

            if (names[i] == SaveSystem.Instance.GetCurrentPlayerName())
            {
                _rankTexts[i].color = Color.green;
                _nameTexts[i].color = Color.green;
                _scoreTexts[i].color = Color.green;
            }
        }
    }
}
