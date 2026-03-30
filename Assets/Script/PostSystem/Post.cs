using UnityEngine;
using System.Collections.Generic;

public class Post
{
    [Header("Post Data")]
    public PostInfo postInfo;
    public List<Comment> comments = new List<Comment>();

    public Post(PostInfo info)
    {
        if(info == null)
        {
            Debug.LogWarning("PostInfo is null.");  
        }
        postInfo = info;
    }

    public PostInfo GetPostInfo()
    {   
        return postInfo;
    }

}