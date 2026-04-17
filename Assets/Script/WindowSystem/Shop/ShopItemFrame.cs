using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemFrame : MonoBehaviour
{
    private Button interactButton;
    [Header("Component needed")]
    public Image background;
    public Image showItem; //May Change to 3D model later
    public Image soldPic;
    public TMP_Text valueText;
    private Item item;



    void Awake()
    {
        Setup();
    }
    
    private void Setup()
    {
        try
        {
            background = GetComponent<Image>();
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
            if(item.isbuy == true) return;
            Shop shop = WindowManager.instance.AccessApp(WindowAppType.CatShop).GetComponent<Shop>();
            shop.Buy(this,item);
        });


    }

    public void ActivateSoldPic()
    {
        /*
        try
        {
            this.soldPic.gameObject.SetActive(true);
        }
        catch (MissingReferenceException)
        {
            Image[] images = gameObject.GetComponentsInChildren<Image>(true);
            foreach(Image image in images)
            {
                if(image.gameObject.tag == "ItemSoldImage")
                {
                    soldPic = image;
                    soldPic.gameObject.SetActive(true);
                }
            }
        }
        */
    }

    public void SetFrame(Item _item)
    {
        string valueInput = $"{_item.value} Coin";
        showItem.sprite = _item.icon;
        valueText.text = valueInput;
        item = _item;
        soldPic.gameObject.SetActive(item.isbuy);
        if (_item.isbuy)
        {
            valueText.gameObject.SetActive(false);
            soldPic.gameObject.SetActive(true);
        }
    }
}