using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuWindow : MonoBehaviour
{
    [SerializeField]    private Button memuButton;
    [SerializeField]    private GameObject menuContent;
    [SerializeField]    private Button nextDayButton;
    [SerializeField]    private Button exitButton;
    [SerializeField]    private Image background;

    private void Start()
    {
        memuButton = GetComponent<Button>();
        memuButton.onClick.AddListener(OnMenuButtonClicked);
        nextDayButton.onClick.AddListener(OnNextDayButtonClicked);
        exitButton.onClick.AddListener(delegate ()
        {
            menuContent.SetActive(false);
            StartCoroutine(OnExitButtonClicked());
        });
        menuContent.SetActive(false);
    }
    public void ChangeSkin(UISetInfo info)
    {
        background.sprite = info.menuBackground;
        memuButton.image.sprite = info.menuButton;
        nextDayButton.image.sprite = info.nextdayButt;
        exitButton.image.sprite = info.mainmenuButt;
        if(info.id != "DF")
        {
            nextDayButton.image.SetNativeSize();
            exitButton.image.SetNativeSize();
            nextDayButton.transform.localScale = new Vector3(0.0003979812f,0.0003979812f,0.0003979812f);
            exitButton.transform.localScale = new Vector3(0.0003979812f,0.0003979812f,0.0003979812f);
            nextDayButton.transform.localPosition = Vector3.zero;
            exitButton.transform.localPosition = Vector3.zero;
        }

    }
    private void OnMenuButtonClicked()
    {
        if(menuContent.activeSelf)
        {
            menuContent.SetActive(false);
        }
        else
        {
            menuContent.SetActive(true);
        }
    }
    private void OnNextDayButtonClicked()
    {
        SummaryViable summary = GameManager.Instance.GetSummaryInfo();
        int day = GameManager.Instance.player.playerViable.currentDay;
        Summary.intence.DisplaySummary(summary,day);
        menuContent.SetActive(false);
    }
    public static IEnumerator OnExitButtonClicked()
    {
        //SaveGame
        CurrentSkin skinInfo = SkinManager.intence.currentSkin;
        string info = JsonUtility.ToJson(skinInfo);
        string filePath = Path.Combine(Application.persistentDataPath,"Savefile");
        File.WriteAllText(filePath,info);
        AudioListener listener = GameObject.FindAnyObjectByType<AudioListener>();
        listener.enabled = false;
        Destroy(listener.gameObject);
        Destroy(WindowManager.instance.gameObject);
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        while (!loadOp.isDone)
        {
            yield return null;
        }
        MenuHandler.Instance.SetUp();
    }
}