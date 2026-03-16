using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="New SceneInfo",menuName ="Skin/Scene/SceneInfo")]
public class SceneSkinInfo : ScriptableObject
{
    public Scene scene;
    [Tooltip("Insert the name of the scene")]
    public string nameID;

    void Awake()
    {
        scene = SceneManager.GetSceneByName(nameID);
    }
}

