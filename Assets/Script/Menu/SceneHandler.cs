using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
    public List<string> loadedScenes = new List<string>();
 
    void Awake()
    {
        SetUp();
    }

    private void SetUp()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }

    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("Test_Setting", LoadSceneMode.Additive);
        loadedScenes.Add("Test_Setting");
    }

    public void UnloadSettingScene()
    {
        SceneManager.UnloadSceneAsync("Test_Setting");
        loadedScenes.Remove("Test_Setting");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Test_MainMenu", LoadSceneMode.Single);
        loadedScenes.Add("Test_MainMenu");
    }
    public void UnloadMainMenuScene()
    {
        SceneManager.UnloadSceneAsync("Test_MainMenu");
        loadedScenes.Remove("Test_MainMenu");
    }
}