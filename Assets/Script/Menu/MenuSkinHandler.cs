using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuSkinHandler : MonoBehaviour
{
    [Header("Menu Element")]
    public Image background;
    public Image logo;
    public Image startButt;
    public Image SettingButt;
    public Image exitButt;

    [Header("Setting Element")]
    public Image settingfarBG;
    public Image settingBg;
    public Image settingText;
    public Image soundText;
    public Image volumeText;
    public Image graphicText;
    public Image brightnessText;
    public Image confirm;
    public Image sideBack;
    public Scrollbar sideSlider;
    public Slider[] sliders;

    void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CurrentSkin skininfo = JsonUtility.FromJson<CurrentSkin>(json);
            SkinManager.intence.currentSkin = skininfo;
        }
        
        string uIId = SkinManager.intence.currentSkin.currentUiSkin;
        if(uIId == null || uIId == "")
            uIId = "DF";
        UISetInfo uISet = SkinManager.intence.GetUISkinByID(uIId);
        ChangeMenuSkin(uISet);
    }

    public void ChangeMenuSkin(UISetInfo info)
    {
        //MainMenu
        background.sprite = info.startgameBackground;
        logo.sprite = info.logo;
        startButt.sprite = info.startButt;
        SettingButt.sprite = info.SettingButt;
        exitButt.sprite = info.exitButt;
        //Setting
        settingfarBG.sprite = info.settingfarBG;
        settingBg.sprite = info.settingBg;
        settingText.sprite = info.settingText;
        soundText.sprite = info.soundText;
        volumeText.sprite = info.volumeText;
        graphicText.sprite = info.graphicText;
        brightnessText.sprite = info.brightnessText;
        confirm.sprite = info.confirm;
        //slider
        GameObject[] sliderBgObjs = GameObject.FindGameObjectsWithTag("Slider Background");
        List<Image> slBg = new List<Image>();
        foreach (GameObject @object in sliderBgObjs)
        {
            Image image = @object.GetComponent<Image>();
            slBg.Add(image);
        }
        foreach (Image img in slBg)
        {
            img.sprite = info.back;
        }
        Image mhandler = sideSlider.handleRect.GetComponent<Image>();
        
        mhandler.sprite = info.mHandler;
        foreach (Slider slider in sliders)
        {
            Image fill = slider.fillRect.GetComponent<Image>();
            Image handler = slider.handleRect.GetComponent<Image>();
            fill.sprite = info.fill;
            handler.sprite = info.handle;
        }
    }

}