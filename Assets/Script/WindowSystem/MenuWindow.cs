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
        exitButton.onClick.AddListener(OnExitButtonClicked);
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
        Summary.intence.DisplaySummary(summary);
        menuContent.SetActive(false);
        Debug.Log("Next Day Button Clicked");
    }
    private void OnExitButtonClicked()
    {
        Debug.Log("Exit Button Clicked");
        //SaveGame
        //SceneManager.LoadScene("MainMenu");
    }
}