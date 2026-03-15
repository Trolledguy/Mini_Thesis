using UnityEngine;
using UnityEngine.SceneManagement;


public class SkinManager : MonoBehaviour
{
    [Header("Scene Skin")]
    public SceneSkinInfo[] skinList;

    public void ChangeScene(SceneSkinInfo info)
    {
        SceneManager.LoadScene(info.id,LoadSceneMode.Single);
    }
}