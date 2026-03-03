using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Summary : MonoBehaviour
{
    public static Summary intence;
    [SerializeField] private TMP_Text totalCustomerText;
    [SerializeField] private TMP_Text totalBadChoiceText;
    [SerializeField] private TMP_Text totalGoodChoiceText;
    [SerializeField] private TMP_Text moneyEarnedText;
    [SerializeField] private Button nextDayButton;

    private void Awake()
    {
        intence = this;

        gameObject.SetActive(false);
        nextDayButton.onClick.AddListener(OnNextDayButtonClicked);
    }

    public void DisplaySummary(SummaryViable viable)
    {
        gameObject.SetActive(true);
        totalCustomerText.text = $"Customers: {viable.ct}";
        totalBadChoiceText.text = $"Bad Choices: {viable.bC}";
        totalGoodChoiceText.text = $"Good Choices: {viable.gC}";
        moneyEarnedText.text = $"Money Earned: ${viable.mn}";

    }

    private void OnNextDayButtonClicked()
    {
        GameManager.Instance.StartNewDay();
        gameObject.SetActive(false); // Hide the summary panel after starting the new day
    }
}

public class SummaryViable
{
    public int ct;
    public int bC;
    public int gC;
    public int mn;

    public SummaryViable(int iCt,int iBc , int iGc,int money)
    {
        ct = iCt;
        bC = iBc;
        gC = iGc;
        mn = money;
    }
}
