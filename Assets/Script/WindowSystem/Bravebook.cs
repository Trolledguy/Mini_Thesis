using UnityEngine;

public class Bravebook : WindowUI
{
    bool isfirstTimeOpen = false;
    [Header("Bravebook Specific Settings")]
    public GameObject contentArea;

    
    public override void ExecuteWindow()
    {
        Debug.Log("Bravebook Executed");
        if(!isfirstTimeOpen)
        {
            isfirstTimeOpen = true;
        }
        
        this.gameObject.SetActive(true);
        
    }

    

}