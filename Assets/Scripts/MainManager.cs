using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public Text GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;
    private bool m_canRestart = false;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        RefreshText();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        //else if (m_GameOver && m_canRestart)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //    }
        //}
    }

    void AddPoint(int point)
    {
        m_Points += point;
        RefreshText();
        SaveSystem.Instance.SetCurrentPlayerScore(m_Points);
    }

    void RefreshText()
    {
        ScoreText.text = $"Score : {m_Points}";

        System.Tuple<int, string> tuple = SaveSystem.Instance.GetRecordInRank(0);
        if (tuple.Item1 <= m_Points)
        {
            BestScoreText.text = $"Best Score : {SaveSystem.Instance.GetCurrentPlayerName()} -> {m_Points}";
        }
        else
        {
            BestScoreText.text = $"Best Score : {tuple.Item2} -> {tuple.Item1}";
        }
    }

    public IEnumerator GameOver()
    {
        m_GameOver = true;
        GameOverText.gameObject.SetActive(true);
        if (SaveSystem.Instance.GetRrcordCount() >= ScoreBoard.MAX_RECORD_COUNT &&
            m_Points < SaveSystem.Instance.GetRecordScoreInRank(SaveSystem.Instance.GetRrcordCount() - 1))
        {
            yield return null;
            m_canRestart = true;
            GameOverText.text = "GAME OVER\nPress Space to restart";
        }
        else
        {
            GameOverText.text = "BREAK RECORD\nPlease wait for loading";
            yield return new WaitForSeconds(3);
            SceneLoader.LoadScene("ScoreBoard");
        }
    }
}
