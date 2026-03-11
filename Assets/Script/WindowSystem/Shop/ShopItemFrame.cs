using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemFrame : MonoBehaviour
{
    private Button interactButton;
    public TMP_Text valueText;


    void Awake()
    {
        this.Setup();
    }
    
    private void Setup()
    {
        interactButton.GetComponent<Button>();
        if(interactButton == null)
            interactButton = gameObject.AddComponent<Button>();
        
        valueText.GetComponentInChildren<TMP_Text>();
        if(valueText == null)
            Debug.LogError($"{gameObject.name} missing Text componet");

        interactButton.onClick.AddListener(delegate ()
        {
            
        });


    }
}