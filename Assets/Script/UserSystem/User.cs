using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New User", menuName = "User System/User")]
public class User : ScriptableObject
{
    [Header("User Info")]
    public string userName;
    public string userID; // Unique identifier for the user
    public UserChat userChatInfo;
    public CatInfo userRequestedCat;
    public List<Post> profilePost = new List<Post>();

    public Sprite profilePicture;
    [Header("User Preferences")]
    public bool isLoveAnimals;

    #if UNITY_EDITOR
    void OnValidate()
    {
        if(userID == "" || userID == null)
        {
            Debug.LogWarning("User ID is not set." + this.name);
            userChatInfo.userChatID = userID;
        }
    }
    #endif

    public Post[] GetProfilePost()
    {
        return profilePost.ToArray();
    }

    public void SetupProfile()
    {
        PostInfo[] _inPost = Resources.LoadAll<PostInfo>("Posts");
        List<Post> outPost = new List<Post>();
        foreach(PostInfo p in _inPost)
        {
            if(p.postTag == PostTag.Profile)
            {
                if(p.postAuthor == this)
                {
                    Post post = new Post(p);
                    outPost.Add(post);
                }
            }
        }

        if(outPost.Count < 1)
        {
            Debug.LogError($"{this.name} Post Does not set Propebly");
            return;
        }
        profilePost = outPost;
    }


}