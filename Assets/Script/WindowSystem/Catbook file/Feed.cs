using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Feed : MonoBehaviour
{
    [Header("Feed Template Data")]
    public Post post;
    public FeedTemplate feedTemplate; //Reference to the feed template UI elements

    public void SetUpFeed(Post postData , FeedTemplate template)
    {
        post = postData;
        feedTemplate = template;
        feedTemplate.SetUpTemplate(post);
    }

}