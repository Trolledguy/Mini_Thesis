using UnityEngine;
using Ink;
using Ink.UnityIntegration;
using NUnit.Framework;
using UnityEngine.UI;

public class Catbook : WindowUI
{
    [Header("Catbook Specific Settings")]
    [SerializeField]
    private RectTransform contentRect; // The RectTransform of the content area where feeds will be spawned
    [SerializeField]
    private RectTransform[] feedsposition = new RectTransform[2]; //positions to spawn feeds into
    private PostStatus currentPostStatus; // To keep track of the current post status for feed management
    [Header("Page Settings")]
    public ProfilePage profilePage;

    [SerializeField]    private Button activeProfileButton;
    [SerializeField]    private Button deactiveProfileButton;

    //For Testing Purposes

    //

    [Header("Feed Positions")]
    private Feed[] feeds = new Feed[3]; // Array to hold references to the spawned feeds


    protected override void Start()
    {
        this.SettUp();
    }
    void Update()
    {
        if(contentRect.anchoredPosition.y > 190f)
        {
            SetContentParentHight(PostStatus.Unscrollable);
            contentRect.anchoredPosition = Vector2.zero;
            UpdateFeed(PostStatus.Unscrollable);
        }
    }

    public void UpdateFeed(PostStatus postStatus) //Call after approving or denying a cat and ContentRect Y > 190, to update the feed based on the post status
    {
        int currentFeedCount = GameManager.Instance.GetFeedCount();
        if(currentFeedCount >= GameManager.Instance.GetFeedRequiredForCurrentDay())
        {
            Debug.Log("Day End");
            //TODO : Implement day transition logic here 
            return;
        }
        switch (postStatus)
        {
            case PostStatus.Scrollable:
                currentPostStatus = PostStatus.Scrollable;
                SetContentParentHight(PostStatus.Scrollable);
                feeds[0] = feeds[2]; // Move the current feed to the scrollable position
                SetFeedPosition(feeds[0], 0);
                feeds[1].gameObject.SetActive(true); // Activate the next feed in line
                feeds[2] = null; // Clear the unscrollable feed reference
                
                break;
            case PostStatus.Unscrollable:
                currentPostStatus = PostStatus.Unscrollable;
                SetContentParentHight(PostStatus.Unscrollable);
                feeds[2] = feeds[1]; // Move the current feed to the unscrollable position
                feeds[1] = null; // Clear the scrollable feed reference
                RemoveFeed(0); 
                SetFeedPosition(feeds[2]);
                SpawnFeeds(1); // Spawn a new feed for the scrollable position
                
                break;
            default:
                Debug.LogError("Invalid post status.");
                break;
        }

        contentRect.anchoredPosition = Vector2.zero;
        GameManager.Instance.IncrementFeedCounter();
    }
    public void ResetFeed()
    {
        ClearFeed();
        SpawnFeeds(2);
    }



    protected override void SettUp()
    {
        base.SettUp();
        if(contentRect == null)
        {
            Debug.LogError("ContentRect is not assigned in Catbook.");
            return;
        }

        currentPostStatus = PostStatus.Unscrollable;
        SpawnFeeds(2); //Spawn Setup

        activeProfileButton.onClick.AddListener(() => profilePage.gameObject.SetActive(true));
        deactiveProfileButton.onClick.AddListener(() => profilePage.gameObject.SetActive(false));

        //Debug
        

    }
    private void ClearFeed()
    {
        for(int i =0;i < feeds.Length;i++)
        {
            if(feeds[i] == null)
            {
                continue;
            }
            Destroy(feeds[i].gameObject);
            feeds[i] = null;
        }
    }

    private void SpawnFeeds(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            
            GameObject feedObject = Instantiate(UIManager.feedTemplatePrefab.gameObject, feedsposition[1].transform);
            feedObject.SetActive(false);
            feedObject.AddComponent<Feed>();

            FeedTemplate feedTemplate = feedObject.GetComponent<FeedTemplate>();
            Feed feed = feedObject.GetComponent<Feed>();
            Post postData = PostManager.instance.GetRandomPost();
            feed.SetUpFeed(postData , feedTemplate);

            //Todo Fix this part, need to set feed position based on post status, and also need to set content parent height based on post status
            if(currentPostStatus == PostStatus.Unscrollable && feeds[2] == null)
            {
                SetContentParentHight(PostStatus.Unscrollable);
                feeds[2] = feed;
                SetFeedPosition(feed);
                feedObject.SetActive(true);
                
                continue;
            }

        
            if(IsPositionAvailable(1))
            {
                feeds[1] = feed;
                SetFeedPosition(feed, 1);
                feedObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No available position for new feed.");
            }
        }
    }
    private void SetContentParentHight(PostStatus postStatus)
    {
        switch (postStatus)
        {
            case PostStatus.Scrollable:
                contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 400f);
                break;
            case PostStatus.Unscrollable:
                contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 200f);
                break;
            default:
                Debug.LogError("Invalid post status.");
                break;
        }
    }

    private void SetFeedPosition(Feed feed, int index)
    {
        feed.transform.SetParent(feedsposition[index]);
        feed.transform.localPosition = Vector3.zero;
        feed.transform.localScale = Vector3.one;
        feeds[index] = feed;
    }
    private void SetFeedPosition(Feed feed)
    {
        feed.transform.SetParent(contentRect);
        feed.transform.localPosition = new Vector3(100f, -100f, 0f);
        feed.transform.localScale = Vector3.one;
    }

    private void RemoveFeed(int index)
    {
        if(feeds[index] == null)
            return;
        Destroy(feeds[index].gameObject);
        feeds[index] = null;
    }

    private bool IsPositionAvailable(int index)
    {
        if(feeds[index] == null)
            return true;
        else
            return false;
    }

}