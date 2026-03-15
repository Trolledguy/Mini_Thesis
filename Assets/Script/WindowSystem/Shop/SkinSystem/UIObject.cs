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
        
        FeedTemplate feedTemplate = (FeedTemplate)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefab/Feed_Template.prefab",typeof (FeedTemplate));
        feedTemplate.background.sprite = uISetInfo.feedBackground;
        feedTemplate.likeButton.image.sprite = uISetInfo.likeButton;
        EditorUtility.SetDirty(feedTemplate);

        CatProfile catProfile = (CatProfile)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefab/Cat_Profile.prefab",typeof (CatProfile));
        catProfile.ChangeSkin(uISetInfo);
        EditorUtility.SetDirty(catProfile);

        ChatBlubbleTemplate chatBlubble = (ChatBlubbleTemplate)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefab/Chat_Bubble_Template.prefab",typeof (ChatBlubbleTemplate));
        chatBlubble.chatBorder.sprite = uISetInfo.chatBorder;
        EditorUtility.SetDirty(chatBlubble);

        ShopItemFrame itemFrame = (ShopItemFrame)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefab/ItemFrame.prefab",typeof (ShopItemFrame));
        itemFrame.bg.sprite = uISetInfo.itemframe;
        EditorUtility.SetDirty(itemFrame);

        Debug.Log("Skin Changed");
    }

}

