using UnityEngine;
using System.Collections.Generic;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    [Header("Apps List")]
    public WindowUI[] allApps; //For regis an app
    
    public Dictionary<WindowAppType,WindowUI> apps = new Dictionary<WindowAppType, WindowUI>();
    public CatProfile catProfilePrefab;


    public RectTransform sizeReference;
    public Canvas windowCanvas;


    void Awake()
    {
        Setup();
    }

    public WindowUI AccessApp(WindowAppType type)
    {
        return apps[type].GetComponent<WindowUI>();
    }

    private Dictionary<WindowAppType, WindowUI> CreateAppAccess()
    {
        Dictionary<WindowAppType,WindowUI> applist = new Dictionary<WindowAppType, WindowUI>();
        foreach(WindowUI w in allApps)
        {
            if(!apps.ContainsKey(w.windowCode))
            {
                WindowAppType id = w.windowCode;
                applist.Add(id,w);
            }
            else
            {
                Debug.LogWarning("Dulplicated Key Found.");
            }
        }
        return applist;
    }


    private void Setup()
    {
        if (instance != this)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        sizeReference = GetComponent<RectTransform>();
        windowCanvas = GetComponentInParent<Canvas>();
        catProfilePrefab = Resources.Load<CatProfile>("Prefab/Cat_Profile");

        apps = CreateAppAccess();

    }
}