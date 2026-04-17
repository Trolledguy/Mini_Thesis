using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    [Header("Background Object")]
    private Image computerBackGround;

    public void ChangeSkin(UISetInfo uISetInfo)
    {
        if(!(SceneManager.GetActiveScene().name == "MainMenu"))
        {
            computerBackGround = GameObject.FindGameObjectWithTag("WindowBackground").GetComponent<Image>();

            Catbook catbook = WindowManager.instance.AccessApp(WindowAppType.Catbook).GetComponent<Catbook>();
            Chat chat = WindowManager.instance.AccessApp(WindowAppType.CatChat).GetComponent<Chat>();
            Shop shop = WindowManager.instance.AccessApp(WindowAppType.CatShop).GetComponent<Shop>();

            computerBackGround.sprite = uISetInfo.backGround;

            catbook.ChangeSkin(uISetInfo.GetSkinByType(WindowAppType.Catbook),uISetInfo);
            chat.ChangeSkin(uISetInfo.GetSkinByType(WindowAppType.CatChat));
            shop.ChangeSkin(uISetInfo.GetSkinByType(WindowAppType.CatShop), uISetInfo);
            
            FeedTemplate feedTemplate = Resources.Load<FeedTemplate>("Prefab/FeedTemplate");
            feedTemplate.feedBackground.sprite = uISetInfo.feedBackground;
            feedTemplate.likeButton.image.sprite = uISetInfo.likeButton;
            feedTemplate.commentButton.image.sprite = uISetInfo.commentButton;
            FeedTemplate[] activeFeed = FindObjectsByType<FeedTemplate>(FindObjectsSortMode.None);
            foreach (FeedTemplate feed in activeFeed)
            {
                feed.feedBackground.sprite = uISetInfo.feedBackground;
                feed.likeButton.image.sprite = uISetInfo.likeButton;
                feed.commentButton.image.sprite = uISetInfo.commentButton;
            }
            

            CatProfile catProfile = Resources.Load<CatProfile>("Prefab/Cat_Profile");
            catProfile.ChangeSkin(uISetInfo);
            CatProfile[] activeCatprofile = FindObjectsByType<CatProfile>(FindObjectsSortMode.None);
            foreach (CatProfile profile in activeCatprofile)
            {
                profile.ChangeSkin(uISetInfo);
            }

            ChatBlubbleTemplate chatBlubble = Resources.Load<ChatBlubbleTemplate>("Prefab/ChatBubbleTemplate");
            chatBlubble.chatBorder.sprite = uISetInfo.chatBorder;
            ChatBlubbleTemplate[] activeBubbel = FindObjectsByType<ChatBlubbleTemplate>(FindObjectsSortMode.None);
            foreach (ChatBlubbleTemplate activeB in activeBubbel)
            {
                activeB.chatBorder.sprite = uISetInfo.chatBorder;
            }

            ShopItemFrame itemFrame = Resources.Load<ShopItemFrame>("Prefab/ItemFrame");
            itemFrame.background.sprite = uISetInfo.itemframe;
            ShopItemFrame[] activeItemframes = FindObjectsByType<ShopItemFrame>(FindObjectsSortMode.None);
            foreach (ShopItemFrame frame in activeItemframes)
            {
                frame.background.sprite = uISetInfo.itemframe;
            }

            ChatHistory chatHistory = Resources.Load<ChatHistory>("Prefab/ChatHistoryTemplate");
            chatHistory.backgroundImage.sprite = uISetInfo.chatHistoryTemplate;
            ChatHistory[] activeHistory = FindObjectsByType<ChatHistory>(FindObjectsSortMode.None);
            foreach (ChatHistory history in activeHistory)
            {
                history.backgroundImage.sprite = uISetInfo.chatHistoryTemplate;
            }

            CommentTemplate commentTemplate = Resources.Load<CommentTemplate>("Prefab/CommentTemplate");
            commentTemplate.background.sprite = uISetInfo.commentSkin;
            CommentTemplate[] activeComment = FindObjectsByType<CommentTemplate>(FindObjectsSortMode.None);
            foreach (CommentTemplate comment in activeComment)
            {
                comment.background.sprite = uISetInfo.commentSkin;
            }
        }
        else
        {
            MenuSkinHandler skinHandler = FindAnyObjectByType<MenuSkinHandler>();
        }
        SkinManager.intence.currentSkin.currentUiSkin = uISetInfo.id;
        Debug.Log("Skin Changed");
    }

}

