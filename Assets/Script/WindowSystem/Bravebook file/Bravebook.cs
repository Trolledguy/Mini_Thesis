using UnityEngine;

public class Bravebook : WindowUI
{
    bool isfirstTimeOpen = false;
    [Header("Bravebook Specific Settings")]
    public GameObject contentArea;
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

    private void LoadFeeds()
    {
        
    }



    

}