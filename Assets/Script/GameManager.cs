using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [Header("Player Settings")]
    public Player player;
    private PlayerViable playerViable;
    private float currentTime;

    void Update()
    {
        RunTimeDayCycle();
    }

    private void Start() //Used Start after Awake because PlayerViable might need to be initialized in gamemanager first.
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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