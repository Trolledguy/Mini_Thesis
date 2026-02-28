using UnityEngine;

public class Post
{
    [Header("Post Data")]
    public PostInfo postInfo;

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