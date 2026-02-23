using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    [Header("Apps List")]
    [SerializeField]
    private WindowUI[] allApps;
    
    public Dictionary<WindowAppType,WindowUI> apps = new Dictionary<WindowAppType, WindowUI>();
    public CatProfile catProfilePrefab;


    public RectTransform sizeReference;
    public Canvas windowCanvas;
    public float screenWidth;
    public float screenHeight;

    

    void Start()
    {
        Setup();
        screenWidth = sizeReference.sizeDelta.x;
        screenHeight = sizeReference.sizeDelta.y;
    }

    public Catbook AccessCatbook()
    {
        return apps[WindowAppType.Catbook].GetComponent<Catbook>();
    }

    public Chat AccessChat()
    {
        return apps[WindowAppType.CatChat].GetComponent<Chat>();
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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sizeReference = GetComponent<RectTransform>();
        windowCanvas = GetComponentInParent<Canvas>();
        catProfilePrefab = Resources.Load<CatProfile>("Prefab/Cat_Profile");

        apps = CreateAppAccess();

    }
}