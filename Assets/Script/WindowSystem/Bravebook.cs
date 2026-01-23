using UnityEngine;

public class Bravebook : WindowUI
{
    bool isfirstTimeOpen = false;
    
    

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