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

        }

    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("Test_Setting", LoadSceneMode.Additive);
        loadedScenes.Add("Test_Setting");
    }

    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
        loadedScenes.Remove(name);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainMap", LoadSceneMode.Single);
        loadedScenes.Add("MainMap");
    }
}