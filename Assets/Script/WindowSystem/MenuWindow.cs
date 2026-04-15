using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuWindow : MonoBehaviour
{
    private Button memuButton;
    [SerializeField]    private GameObject menuContent;
    [SerializeField]    private Button nextDayButton;
    [SerializeField]    private Button exitButton;

    private void Start()
    {
        memuButton = GetComponent<Button>();
        memuButton.onClick.AddListener(OnMenuButtonClicked);
        nextDayButton.onClick.AddListener(OnNextDayButtonClicked);
        exitButton.onClick.AddListener(delegate ()
        {
            StartCoroutine(OnExitButtonClicked());
        });
        menuContent.SetActive(false);
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
    private IEnumerator OnExitButtonClicked()
    {
        //SaveGame
        AudioListener listener = GameObject.FindAnyObjectByType<AudioListener>();
        listener.enabled = false;
        Destroy(listener.gameObject);
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("Test_MainMenu", LoadSceneMode.Single);
        while (!loadOp.isDone)
        {
            yield return null;
        }
        MenuHandler.Instance.SetUp();
    }
}