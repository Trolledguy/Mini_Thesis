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


    //Test Zone
    [Header("Test Zone")]
    public Button testButton;
    public SceneSkinInfo skinTest; 

    void Awake()
    {
        if(intence != this)
        {
            Destroy(intence);
            intence = this;
            DontDestroyOnLoad(gameObject);   
        }

        skinInfos = CreateSkinList();
        testButton.onClick.AddListener(delegate ()
        {
            GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Game Logical");
            
            AssignDontDestroy(playerObj);
            StartCoroutine(ChangeScene(skinTest));
        });
    }
    public void ChangeUI() //TODO : Change to selectable skin through ID/Name
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

    public IEnumerator ChangeScene(SceneSkinInfo info)
    {
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
        if(uIs.Count < 0)
            return null;
        
        int r = Random.Range(0,uIs.Count);
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