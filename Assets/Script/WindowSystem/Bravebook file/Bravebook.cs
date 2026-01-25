using UnityEngine;

public class Bravebook : WindowUI
{
    bool isfirstTimeOpen = false;
    [Header("Bravebook Specific Settings")]
    public GameObject contentArea;
    public RectTransform[] feedsposition = new RectTransform[2];
    private FeedTemplate feedTemplatePrefab;
    private Feed[] feeds;

    void OnValidate()
    {
        
    }

    public override void ExecuteWindow()
    {
        Debug.Log("Bravebook Executed");
        if(!isfirstTimeOpen)
        {
            isfirstTimeOpen = true;
        }
        
        this.gameObject.SetActive(true);
        
    }
    protected override void SettUp()
    {
        base.SettUp();
        feedTemplatePrefab = Resources.Load<FeedTemplate>("Prefabs");
    }

    private void SetFeeds()
    {
        GameObject feedObject = Instantiate(feedTemplatePrefab.gameObject, contentArea.transform);
        feedObject.SetActive(false);
    }



    

}