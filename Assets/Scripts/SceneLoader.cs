using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public PersistentDataSO Data;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void StartGame()
    {
        LoadScene("Main");
    }

    public static void ExitGame()
    {
        Application.Quit();
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
