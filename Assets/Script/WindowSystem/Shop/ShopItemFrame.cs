using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemFrame : MonoBehaviour
{
    private Button interactButton;
    [Header("Component needed")]
    public Image bg;
    public Image sp; //May Change to 3D model later
    public TMP_Text valueText;


    void Awake()
    {
        this.Setup();
    }
    
    private void Setup()
    {
        try
        {
            bg = GetComponent<Image>();
            interactButton = GetComponent<Button>();
            valueText.GetComponentInChildren<TMP_Text>();
        }
        catch (NullReferenceException)
        {
            if(interactButton == null)
            interactButton = gameObject.AddComponent<Button>();
            if(valueText == null)
            Debug.LogError($"{gameObject.name} missing Text componet");
        }

        interactButton.onClick.AddListener(delegate ()
        {
            Debug.Log("Buy Pressed");
        });


    }

    public void SetFrame(Item _item)
    {
        string valueInput = $"{_item.value} Coin";
        sp.sprite = _item.icon;
        valueText.text = valueInput;
    }
}