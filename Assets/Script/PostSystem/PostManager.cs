using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    public static PostManager instance;
    public Dictionary<string, Post> posts = new Dictionary<string, Post>();

    private void Awake()
    {
        Setup();
    }

    private Dictionary<string, Post> CreatePostDictionary()
    {
        PostInfo[] postList = Resources.LoadAll<PostInfo>("Posts");
        Dictionary<string, Post> newpostDict = new Dictionary<string, Post>();

        foreach (PostInfo postInfo in postList)
        {
            Post post = new Post(postInfo);
            if (!newpostDict.ContainsKey(postInfo.postID))
            {
                newpostDict.Add(postInfo.postID, post);
                Debug.Log("Post added: " + postInfo.postID);
            }
            else
            {
                Debug.LogWarning("Duplicate Post ID found: " + postInfo.postID);
            }
        }
        return newpostDict;
    }

    public Post GetPostByID(string postID)
    {
        if (posts.ContainsKey(postID))
            return posts[postID];
        else
        {
            Debug.LogWarning("Post ID not found: " + postID);
            return null;
        }
    }

    public Post[] GetAllPost()
    {
        Post[] rPosts = posts.Values.ToArray();
        return rPosts;
    }

    public Post GetRandomPost()
    {
        List<Post> postValues = new List<Post>(posts.Values);
        if (postValues.Count == 0)
        {
            Debug.LogWarning("No posts available to select.");
            return null;
        }
        int randomIndex = Random.Range(0, postValues.Count);
        return postValues[randomIndex];
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

        posts = CreatePostDictionary();
    }   
    
}