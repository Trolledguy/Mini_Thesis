using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [Header("Player Settings")]
    public Player player;
    private PlayerViable playerViable;
    private float currentTime;

    private int currectCase = 0;
    private int incurrectCase = 0;

    void Update()
    {
        RunTimeDayCycle();
    }

    public void UpdateScore(bool isCorrect)
    {
        if (isCorrect)
            currectCase++;
        else
            incurrectCase++;
        DebugBox.instance.AddDebugText($"Answer is {(isCorrect ? "Correct" : "Incorrect")}. \n Current Score: {currectCase} Correct, {incurrectCase} Incorrect.");
    }
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
        
    }

    private void RunTimeDayCycle()
    {
        if(playerViable == null) 
        { 
            Debug.LogError("PlayerViable is null in RunTimeDayCycle.");
            return; 
        }

        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Day has ended. Resetting time for next day.");
            //playerViable.timeRemainingPerDay = 100f; 
        }
        
    }
}  