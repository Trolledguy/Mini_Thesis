using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SkinManager : MonoBehaviour
{
    public static SkinManager intence;

    [Header("Scene Skin")]
    public SceneSkinInfo[] skinList;
    public UIObject ui;
    public Dictionary<string,UISetInfo> skinInfos = new Dictionary<string, UISetInfo>();

    public CurrentSkin currentSkin = new CurrentSkin();



    void Awake()
    {
        if(intence != this)
        {
            Destroy(intence);
            intence = this;
            DontDestroyOnLoad(gameObject);   
        }
        
        skinInfos = CreateSkinList();
        
    }
    private void Start()
    {
        UISetInfo dfSkin = GetUISkinByID("DF");
        ui.ChangeSkin(dfSkin);
    }
    public void ChangeUI(string id) 
    {
        if (!skinInfos.ContainsKey(id))
        {
            Debug.LogError("Skin ID Not Found");
            return;
        }
        UISetInfo test = GetUISkinByID(id);
        ui.ChangeSkin(test);
    }
    public void ChangeUI() 
    {
        UISetInfo test = GetRandomSkin();
        ui.ChangeSkin(test);
    }

    public void AssignDontDestroy(GameObject[] objs)
    {
        foreach(GameObject i in objs)
        {
            Debug.Log($"Name : {i.name}");
            DontDestroyOnLoad(i);
        }
        
    }
    public void ToggleChangeScene(SceneSkinInfo skin)
    {
        StartCoroutine(ChangeScene(skin));
        currentSkin.currentSceneSkin = skin.nameID;
    }

    private IEnumerator ChangeScene(SceneSkinInfo info)
    {
        if(info.nameID == "Mainmap")
        {
            StartCoroutine(MenuWindow.OnExitButtonClicked());
            Destroy(WindowManager.instance.gameObject);
            SceneManager.LoadScene(info.nameID);
            yield break;
        }
        GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Game Logical");
        AssignDontDestroy(playerObj);

        InputManager.Instance.gameObject.SetActive(false);
        
        AsyncOperation op = SceneManager.LoadSceneAsync(info.nameID,LoadSceneMode.Single);

        while (!op.isDone)
        {
            Debug.Log("Loading");
            yield return null;
        }

        SceneAnchor sceneAnchor = GameObject.FindAnyObjectByType<SceneAnchor>();
        if(sceneAnchor == null)
        {
            Debug.LogError("Scene Anchor Not Found");
            yield break;
        }

        PlayerCamera playerCamera = PlayerCamera.Instance;
        playerCamera.SetUp();
        StartCoroutine(playerCamera.AdjustFOV(false,100));
        
        

        playerCamera.transform.position = sceneAnchor.cameraPoint.position;
        playerCamera.transform.eulerAngles = new Vector3(0,180,0);

        WindowManager wd = WindowManager.instance; 

        wd.transform.position = sceneAnchor.uiPoint.position;
        wd.transform.localEulerAngles = Vector3.zero;

        InputManager.Instance.gameObject.SetActive(true);
        InputManager.SetInput(true);
        playerCamera.SetZoom(false);
        yield return null;
    }
    public UISetInfo GetUISkinByID(string _id)
    {
        return skinInfos[_id];
    }
    private UISetInfo GetRandomSkin()
    {
        List<UISetInfo> uIs = new List<UISetInfo>(skinInfos.Values);
        if(uIs.Count < 1)
        {
            Debug.LogError("No UI Loaded");
            return null;
        }
        
        int r = UnityEngine.Random.Range(0,uIs.Count);
        return uIs[r];
    }
    private Dictionary<string,UISetInfo> CreateSkinList()
    {
        Dictionary<string,UISetInfo> res = new Dictionary<string,UISetInfo>();
        UISetInfo[] uiSkins = Resources.LoadAll<UISetInfo>("Skin");
        foreach(UISetInfo skin in uiSkins)
        {
            if(!res.ContainsKey(skin.id))
            {
                string k = skin.id;
                res.Add(k,skin);
            }
        }
        return res;
    }

}

[Serializable]
public class CurrentSkin
{
    public string currentUiSkin;
    public string currentSceneSkin;
}