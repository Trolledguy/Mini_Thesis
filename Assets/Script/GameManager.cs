using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [Header("Player Settings")]
    public Player player;
    private PlayerViable playerViable;
    private float currentTime;

    private int feedCounter = 0;
    private int currectCase = 0;
    private int incurrectCase = 0;
    private int currentDayEarned = 0;

    private void Start() //Used Start after Awake because PlayerViable might need to be initialized in gamemanager first.
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        playerViable = player.playerViable;
        if(playerViable == null)
            Debug.LogError("PlayerViable is not initialized in GameManager.");

        currentTime = playerViable.timeRemainingPerDay;
        
        UIManager.Instance.UpdateDayText(playerViable.currentDay);
        UIManager.Instance.SetBlackScreen(1f, false);
    }

    void Update()
    {
        RunTimeDayCycle();
    }

    public void StartNewDay()
    {
        currentDayEarned = 0;
        currentTime = 0;
        playerViable.currentDay++;
        WindowManager.instance.AccessCatbook().gameObject.SetActive(true);
        WindowManager.instance.AccessChat().gameObject.SetActive(true);
        ResetFeedCounter();
        UIManager.Instance.UpdateDayText(playerViable.currentDay);
        UIManager.Instance.SetBlackScreen(1f, false);
        
        WindowManager.instance.AccessCatbook().ResetFeed();
        

        WindowManager.instance.AccessCatbook().gameObject.SetActive(false);
        WindowManager.instance.AccessChat().gameObject.SetActive(false);
    }

    public void UpdateScore(bool isCorrect)
    {
        if (isCorrect)
            currectCase++;
        else
            incurrectCase++;
        DebugBox.instance.AddDebugText($"Answer is {(isCorrect ? "Correct" : "Incorrect")}. \n Current Score: {currectCase} Correct, {incurrectCase} Incorrect.");
    }
    public void AddDayEarned(int _amount)
    {
        currentDayEarned += _amount;
        playerViable.moneyBalance += _amount;
    }
    public SummaryViable GetSummaryInfo()
    {
        SummaryViable summary = new SummaryViable(feedCounter,incurrectCase,currectCase,currentDayEarned);
        return summary;
    }
    public void ResetFeedCounter()
    {
        feedCounter = 0;
    }
    public void IncrementFeedCounter()
    {
        feedCounter++;
    }

    public int GetFeedCount()
    {
        return feedCounter;
    }
    public int GetFeedRequiredForCurrentDay()
    {
        if(playerViable == null)
        {
            Debug.LogError("PlayerViable is not initialized in GameManager.");
            return 0; // Return a default value or handle this case as needed
        }
        return playerViable.GetFeedRequired();
    }

    private void RunTimeDayCycle()
    {

        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Day has ended. Resetting time for next day.");
            
        }
        
    }
}  