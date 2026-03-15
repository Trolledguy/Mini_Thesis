using UnityEngine;
using UnityEngine.SceneManagement;


public class SkinManager : MonoBehaviour
{
    public static SkinManager intence;

    [Header("Scene Skin")]
    public SceneSkinInfo[] skinList;
    public UIObject ui;

    void Awake()
    {
        if(intence != this)
        {
            Destroy(intence);
            intence = this;
            DontDestroyOnLoad(gameObject);   
        }

    }


    public void ChangeScene(SceneSkinInfo info)
    {
        SceneManager.LoadScene(info.id,LoadSceneMode.Single);
    }
}