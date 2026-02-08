using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Feed : MonoBehaviour
{
    [Header("Feed Data")]
    private Post post;
    private FeedTemplate feedTemplate; //Reference to the feed template UI elements

    public void SetUpFeed(Post postData , FeedTemplate template)
    {
        post = postData;
        feedTemplate = template;
        feedTemplate.SetUpTemplate(post);
    }

}