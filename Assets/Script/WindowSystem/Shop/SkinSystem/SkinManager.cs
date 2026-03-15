using System.Collections.Generic;
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

    public Button testButton;

    void Awake()
    {
        if(intence != this)
        {
            Destroy(intence);
            intence = this;
            DontDestroyOnLoad(gameObject);   
        }

        skinInfos = CreateSkinList();
        testButton.onClick.AddListener(ChangeUI);
    }
    public void ChangeUI()
    {
        UISetInfo test = GetRandomSkin();
        ui.ChangeSkin(test);
    }


    public void ChangeScene(SceneSkinInfo info)
    {
        SceneManager.LoadScene(info.id,LoadSceneMode.Single);
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