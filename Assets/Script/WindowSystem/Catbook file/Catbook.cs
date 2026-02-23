using UnityEngine;
using Ink;
using Ink.UnityIntegration;
using NUnit.Framework;
using UnityEngine.UI;

public class Catbook : WindowUI
{
    [Header("Bravebook Specific Settings")]
    [SerializeField]
    private RectTransform[] feedsposition = new RectTransform[2]; //positions to spawn feeds into
    [Header("Page Settings")]
    public ProfilePage profilePage;

    [SerializeField]    private Button activeProfileButton;
    [SerializeField]    private Button deactiveProfileButton;

    [Header("Debug")]
    [SerializeField]    private Button testButton;
    [SerializeField]    private PostInfo TestUser;

    private Feed[] feeds = new Feed[2];
    public static FeedTemplate feedTemplatePrefab;


    protected override void Start()
    {
        this.SettUp();
    }

    public void UpdateFeed(Post postData)
    {
        RemoveFeed(0);
        SpawnFeeds(1);
    }


    protected override void SettUp()
    {
        base.SettUp();
        feedTemplatePrefab = Resources.Load<FeedTemplate>("Prefab/Feed_Template");

        SpawnFeeds(2);

        activeProfileButton.onClick.AddListener(() => profilePage.gameObject.SetActive(true));
        deactiveProfileButton.onClick.AddListener(() => profilePage.gameObject.SetActive(false));

        //Debug
        testButton.onClick.AddListener(() => profilePage.SetProfile(TestUser.postAuthor));

    }

    private void SpawnFeeds(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject feedObject = Instantiate(feedTemplatePrefab.gameObject, feedsposition[1].transform);
            feedObject.SetActive(false);
            feedObject.AddComponent<Feed>();

            FeedTemplate feedTemplate = feedObject.GetComponent<FeedTemplate>();
            Feed feed = feedObject.GetComponent<Feed>();
            Post postData = PostManager.instance.GetRandomPost();
            feed.SetUpFeed(postData , feedTemplate);

        
            if(IsPositionAvailable(i))
            {
                feeds[i] = feed;
                SetFeedPosition(feed, i);
                feedObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No available position for new feed.");
            }
        }
    }

    private void SetFeedPosition(Feed feed, int index)
    {
        feed.transform.SetParent(feedsposition[index]);
        feed.transform.localPosition = Vector3.zero;
        feed.transform.localScale = Vector3.one;
    }

    private void RemoveFeed(int index)
    {
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