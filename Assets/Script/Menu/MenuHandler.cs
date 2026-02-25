using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler Instance { get; private set; }
    [Header("Menu Setting")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button applyButton;

    [Header("Setting")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private Dropdown displayModeDropdown;
    [SerializeField] private Slider brightnessSlider;


    void Awake()
    {
        SetUp();
    }

    private void SetUp()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        List<string> displayOptions = Screen.resolutions.Length > 0 ? new List<string>() : null;
        foreach (Resolution res in Screen.resolutions)
        {
            displayOptions.Add(res.width + "x" + res.height);
        }
        displayModeDropdown.ClearOptions();
        displayModeDropdown.AddOptions(displayOptions);

        startButton.onClick.AddListener(OnStartButtonClicked);
        settingButton.onClick.AddListener(OnSettingButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
        applyButton.onClick.AddListener(OnApplyButtonClicked);

    }
    
    private void OnApplyButtonClicked()
    {
        musicVolume = musicVolumeSlider.value;
        AudioListener.volume = musicVolume;
        RenderSettings.ambientLight = new Color(0f, 0f, 0f, brightnessSlider.value);
        Screen.SetResolution(Screen.resolutions[displayModeDropdown.value].width, Screen.resolutions[displayModeDropdown.value].height, Screen.fullScreen);
    }

    private void OnStartButtonClicked()
    {
        SceneHandler.Instance.LoadMainScene();
    }
    private void OnSettingButtonClicked()
    {
        SceneHandler.Instance.LoadSettingScene();        
    }
    private void OnBackButtonClicked()
    {
        SceneHandler.Instance.UnloadSettingScene();
    }
    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}