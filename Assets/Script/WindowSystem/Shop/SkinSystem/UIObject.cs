using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    [Header("List of UI Object")]
    public Image backGround;

    public void ChangeSkin(UISetInfo uISetInfo)
    {
        Catbook catbook = WindowManager.instance.AccessApp(WindowAppType.Catbook).GetComponent<Catbook>();
        Chat chat = WindowManager.instance.AccessApp(WindowAppType.CatChat).GetComponent<Chat>();
        Shop shop = WindowManager.instance.AccessApp(WindowAppType.CatShop).GetComponent<Shop>();

        backGround.sprite = uISetInfo.backGround;

        catbook.ChangeSkin(uISetInfo.GetSkinByType(WindowAppType.Catbook));
        chat.ChangeSkin(uISetInfo.GetSkinByType(WindowAppType.CatChat));
        shop.ChangeSkin(uISetInfo.GetSkinByType(WindowAppType.CatShop), uISetInfo);
        
        FeedTemplate feedTemplate = Resources.Load<FeedTemplate>("Prefab/FeedTemplate");
        feedTemplate.feedBackground.sprite = uISetInfo.feedBackground;
        feedTemplate.likeButton.image.sprite = uISetInfo.likeButton;
        

        CatProfile catProfile = Resources.Load<CatProfile>("Prefab/Cat_Profile");
        catProfile.ChangeSkin(uISetInfo);

        ChatBlubbleTemplate chatBlubble = Resources.Load<ChatBlubbleTemplate>("Prefab/ChatBubbleTemplate");
        chatBlubble.chatBorder.sprite = uISetInfo.chatBorder;

        ShopItemFrame itemFrame = Resources.Load<ShopItemFrame>("Prefab/ItemFrame");
        itemFrame.background.sprite = uISetInfo.itemframe;

        Debug.Log("Skin Changed");
    }

}

