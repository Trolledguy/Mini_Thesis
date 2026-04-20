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
    public Sprite nextdayButt;
    public Sprite mainmenuButt;
    public WindowSkin[] windowSkins;
    
    [Header("Catbook Asset")]
    public Sprite feedBackground;
    public Sprite likeButton;
    public Sprite commentButton;
    public Sprite catProfileBg;
    public Sprite appProveButton;
    public Sprite denyButton;
    public Sprite chatHistoryBG;
    public Sprite chatHistoryTemplate;
    public Sprite commentSkin;

    [Header("Chat Asset")]
    public Sprite chatBorder;

    [Header("Shop Asset")]
    public Sprite itemframe;
    public Sprite deskTopB;
    public Sprite roomB;
    public Sprite catB;
    public Sprite foodB;

    [Header("Menu Asset")]
    public Sprite startgameBackground;
    public Sprite logo;
    public Sprite startButt;
    public Sprite SettingButt;
    public Sprite exitButt;

    [Header("Setting Element")]
    public Sprite settingfarBG;
    public Sprite settingBg;
    public Sprite settingText;
    public Sprite soundText;
    public Sprite volumeText;
    public Sprite graphicText;
    public Sprite brightnessText;
    public Sprite confirm;
    
    [Header("Slider Asset")]
    public Sprite back;
    public Sprite fill;
    public Sprite handle;
    [Header("Main slider Asset")]
    public Sprite msideBack;
    public Sprite mHandler;
    
    

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

