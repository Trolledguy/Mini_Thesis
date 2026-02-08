using UnityEngine;
using Ink.Runtime;


[CreateAssetMenu(fileName = "New User Chat", menuName = "User System/User Chat")]
public class UserChat : ScriptableObject
{
    [SerializeField]
    private TextAsset inkJSONAsset;
    public string userChatID;
    public Story userStory;


    void OnValidate()
    {
        #if UNITY_EDITOR
        userStory = new Story(inkJSONAsset.text);
        #endif
    }
}