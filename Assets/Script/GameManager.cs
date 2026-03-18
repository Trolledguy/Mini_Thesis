using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [Header("Player Settings")]
    public Player player;
    private PlayerViable playerViable;
    public Cat selectCat;
    private float currentTime;

    private int feedCounter = 0;
    private int currectCase = 0;
    private int incurrectCase = 0;
    private int currentDayEarned = 0;


    private User _User;
    public string currentUserID
    {
        get
        {
            return _User.userID;
        }
        set
        {
            _User = UserManager.intensce.GetUserByID(value);
        }
    }

    private void Start() //Used Start after Awake because PlayerViable might need to be initialized in gamemanager first.
    {
        if (Instance != this)
        {
            Instance = this;
        }



        playerViable = player.playerViable;
        if(playerViable == null)
            Debug.LogError("PlayerViable is not initialized in GameManager.");

        currentTime = playerViable.timeRemainingPerDay;
        
        CatBoard catBoard = FindAnyObjectByType<CatBoard>();
        catBoard.OnNewDay();
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
        Catbook catbook = WindowManager.instance.AccessApp(WindowAppType.Catbook).GetComponent<Catbook>();
        Chat chat = WindowManager.instance.AccessApp(WindowAppType.CatChat).GetComponent<Chat>();
        catbook.gameObject.SetActive(true); //Set Active to get info and prevent error
        chat.gameObject.SetActive(true);
        ResetFeedCounter();
        UIManager.Instance.UpdateDayText(playerViable.currentDay);
        UIManager.Instance.SetBlackScreen(1f, false);
        
        catbook.ResetFeed();
        

        catbook.gameObject.SetActive(false);
        chat.gameObject.SetActive(false);
    }

    public void UpdateScore(Cat cat,User user)
    {
        if(cat.catInfo.catIdentity == user.userIdentity)
        {
            currectCase++;
        }
        else
        {
            incurrectCase++;
        }

        DebugBox.UpdateScore(currectCase);
        DebugBox.AddDebugText($"Answer is {(cat.catInfo.catIdentity == user.userIdentity ? "Correct" : "Incorrect")}. \n Current Score: {currectCase} Correct, {incurrectCase} Incorrect.");
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