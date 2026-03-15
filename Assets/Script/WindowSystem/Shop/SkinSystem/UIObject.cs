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
        
        FeedTemplate feedTemplate = AssetDatabase.LoadAssetAtPath<FeedTemplate>("Assets/Resources/Prefab");
        feedTemplate.background.sprite = uISetInfo.feedBackground;
        feedTemplate.likeButton.image.sprite = uISetInfo.likeButton;
        EditorUtility.SetDirty(feedTemplate);

        CatProfile catProfile = AssetDatabase.LoadAssetAtPath<CatProfile>("Assets/Resources/Prefab");
        catProfile.ChangeSkin(uISetInfo);
        EditorUtility.SetDirty(catProfile);

        ChatBlubbleTemplate chatBlubble = AssetDatabase.LoadAssetAtPath<ChatBlubbleTemplate>("Assets/Resources/Prefab");
        chatBlubble.chatBorder.sprite = uISetInfo.chatBorder;
        EditorUtility.SetDirty(chatBlubble);

        ShopItemFrame itemFrame = AssetDatabase.LoadAssetAtPath<ShopItemFrame>("Assets/Resources/Prefab");
        itemFrame.bg.sprite = uISetInfo.itemframe;
        EditorUtility.SetDirty(itemFrame);

        Debug.Log("Skin Changed");
    }

}

