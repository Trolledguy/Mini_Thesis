using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Feed : MonoBehaviour
{
    [Header("Feed Template Data")]
    public Post post;
    public Feedposition feedposition;
    public FeedTemplate feedTemplate;

    public void SetUpFeed(Post postData, Feedposition positionData)
    {
        post = postData;
        feedposition = positionData;
        feedTemplate.SetUpTemplate(post);
    }

}