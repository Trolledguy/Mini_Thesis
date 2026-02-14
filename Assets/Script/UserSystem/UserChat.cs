using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New User Chat", menuName = "User System/User Chat")]
public class UserChat : ScriptableObject
{
    [Header("Text Asset File")]
    [SerializeField]
    private TextAsset inkJSONAsset;
    [Header("User Identifier")]
    public string userChatID;
    public Story userStory;
    [Header("Sprite Use in Textfile")]
    public Sprite[] sendImage = new Sprite[0];
    private Dictionary<string,Sprite> allImage = new Dictionary<string, Sprite>();

    private string selectImageID;



    public void SetupChat()
    {
        userStory = new Story(inkJSONAsset.text);
        allImage = CreateSpriteList();
    }

    

    public Sprite GetCurrentImage()
    {
        Debug.Log($"Image ID : {selectImageID}");
        return allImage[selectImageID];
    }


    public void HandleTags(List<string> _tags)
    {
        Debug.Log($"Tag Count : {_tags.Count}");
        selectImageID = "";
        int errorCount = 0;
        foreach(var t in _tags)
        {
            var split = t.Split("=");
            if(split.Length != 2) 
            {
                errorCount++;
                Debug.LogError($"{errorCount} Tags error in text asset at : {this.name}");
                continue;
            }
            Debug.Log($"Tag : {split[0]}");
            Debug.Log($"Value : {split[1]}");
            if(split[0] == "IMG"){selectImageID = split[1];}
        }
    }

    private Dictionary<string,Sprite> CreateSpriteList()
    {
        Dictionary<string,Sprite> dic = new Dictionary<string, Sprite>();
        int i =0;
        foreach(Sprite s in sendImage)
        {
            dic.Add($"IMG{i}",s);
            Debug.Log($"ADD : IMG{i}");
            i++;
        }
        return dic;
    }

    public static bool IsTagChange(List<string> oldT, List<string> newT)
    {
        if(oldT == null)
            return true;
        
        foreach(string i in newT)
        {
            if(oldT.Contains(i))
                return true;   
        }
        return false;
    }

    
}