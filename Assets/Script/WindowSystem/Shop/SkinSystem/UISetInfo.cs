using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New UISkinInfo",menuName ="Skin/Scene/UISkinInfo")]
public class UISetInfo : ScriptableObject
{
    [Header("ID")]
    public string id;
    [Header("Main Window Asset")]
    public Sprite backGround;
    public Sprite menuButton;
    public Sprite menuBackground;
    public WindowSkin[] windowSkins;
    
    [Header("Catbook Asset")]
    public Sprite feedBackground;
    public Sprite likeButton;
    public Sprite catProfileBg;
    public Sprite appProveButton;
    public Sprite denyButton;

    [Header("Chat Asset")]
    public Sprite chatBorder;

    [Header("ShopAsset")]
    public Sprite itemframe;
    public Sprite deskTopB;
    public Sprite roomB;
    public Sprite catB;
    public Sprite foodB;

    public WindowSkin GetSkinByType(WindowAppType appType)
    {
        foreach(WindowSkin skin in windowSkins)
        {
            if(appType == skin.type)
            {
                return skin;
            }
        }
        Debug.LogError("Skin type Not found");
        return new WindowSkin();
    }
}



[System.Serializable]
public struct WindowSkin
{
    public WindowAppType type;
    public Sprite window;
    public Sprite desktopIcon;
    public Sprite closeIcon;
}

