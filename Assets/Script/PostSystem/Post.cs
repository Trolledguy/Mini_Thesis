using UnityEngine;

public class Post
{
    [Header("Post Data")]
    public PostInfo postInfo;
    public PostStatus postStatus;

    public Post(PostInfo info)
    {
        if(info == null)
        {
            Debug.LogWarning("PostInfo is null.");  
        }
        postInfo = info;
        postStatus = PostStatus.Unscrollable;
    }
    public void SetPostStatus(PostStatus status)
    {
        postStatus = status;
    }

    public PostInfo GetPostInfo()
    {   
        return postInfo;
    }
    public PostStatus GetPostStatus()
    {
        return postStatus;
    }
}