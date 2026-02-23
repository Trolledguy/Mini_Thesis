using System.Collections.Generic;
using UnityEngine;

public class ProfilePage : MonoBehaviour
{
    [SerializeField]
    private RectTransform contentParent;


    public void SetProfile(User user)
    {
        Post[] posts = user.GetProfilePost();
        List<FeedTemplate> newFeeds = new List<FeedTemplate>();
        int feedCount = 0;
        int index = 0;
        foreach(Post p in posts)
        {
            FeedTemplate post = SpawnPostAndGetPost(p);
            newFeeds.Add(post);
            AddHight();
            feedCount++;
        }
        foreach(FeedTemplate f in newFeeds)
        {
            SetPosition(feedCount, index, f.GetComponent<RectTransform>());
            index++;
        }
    }

    private void SetPosition(int feedCount, int index, RectTransform reTransform)
    {
        float center = contentParent.rect.height/2;
        int middleIndex = index/2;

        if(index < middleIndex)
        {
            reTransform.localPosition = new Vector3(0 + 100, (index * 200) -100,0);
        }
        else if(index > middleIndex)
        {
            reTransform.localPosition = new Vector3(0 + 100, -(index * 200) -100,0);
        }
        else
        {
            reTransform.localPosition = new Vector3(0 + 100,center - (feedCount*100) -100,0);
        }
        reTransform.localRotation = Quaternion.identity;
    }

    private void AddHight()
    {
        contentParent.sizeDelta += new Vector2(0, 200);
    }

    private FeedTemplate SpawnPostAndGetPost(Post post)
    {
        FeedTemplate prefab = Catbook.feedTemplatePrefab;
        GameObject obj = Instantiate(prefab.gameObject);
        obj.transform.SetParent(contentParent);
        obj.transform.localScale = Vector3.one;
        FeedTemplate fPrefab = obj.GetComponent<FeedTemplate>();
        fPrefab.SetUpTemplate(post);

        return fPrefab;
    }
}