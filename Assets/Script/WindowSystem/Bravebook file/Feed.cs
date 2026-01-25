using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Feed : MonoBehaviour
{
    [Header("Feed Template Data")]
    public Post post;
    public FeedTemplate feedTemplate;

    public void SetUpFeed(Post postData)
    {
        post = postData;
        feedTemplate.SetUpTemplate(post);
    }

}