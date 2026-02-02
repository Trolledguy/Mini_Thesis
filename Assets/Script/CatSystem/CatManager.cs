using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public static CatManager instance;

    public Dictionary<string, Cat> catList = new Dictionary<string, Cat>();

    private void Awake()
    {

        Setup();
    }

    private Dictionary<string, Cat> CreateCatDictionary()
    {
        CatInfo[] catList = Resources.LoadAll<CatInfo>("Cats");
        Dictionary<string, Cat> newCatDict = new Dictionary<string, Cat>();

        foreach (CatInfo catInfo in catList)
        {
            Cat cat = new Cat(catInfo);
            if (!newCatDict.ContainsKey(catInfo.catID))
            {
                newCatDict.Add(catInfo.catID, cat);
                Debug.Log("Cat added: " + catInfo.catID);
            }
            else
            {
                Debug.LogWarning("Duplicate Cat ID found: " + catInfo.catID);
            }
        }
        return newCatDict;
    }



    private void Setup()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        catList = CreateCatDictionary();
    }
    
}