using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Post Info", menuName = "Post System/Post Info")]
public class PostInfo : ScriptableObject
{
    [Header("Post Details")]
    public User postAuthor;
    public string postID; // Unique identifier for the post
    public PostTag postTag;
    
    [Header("Post Time")]
    public PostTime postTime;
    [SerializeField] private int day;
    [SerializeField] private int month;
    [SerializeField] private int year;
    [Header("Post Likes")]
    [SerializeField] private int defaultLikeCount;
    public int likeCount;

    [Header("Post Content")]
    public string postContent;
    public Sprite postImage;

    void OnValidate()
    {
        #if UNITY_EDITOR
        if (day < 1 || day > 31)
            day = 1;
        if (month < 1 || month > 12)
            month = 1;
        if (year < 2000)
            year = 2000;
        #endif

        postTime = new PostTime(day, month, year);
        likeCount = defaultLikeCount;

        if(postID == ""||postID == null)
        {
            Debug.LogWarning("Post ID is not set." + this.name);
        }
        if(postAuthor == null)
        {
            Debug.LogWarning("Post Author is not assigned." + this.name);
        }

        if(defaultLikeCount < 0) 
        {
            defaultLikeCount = 0;
            likeCount = 0;
        }
        
    }

}

