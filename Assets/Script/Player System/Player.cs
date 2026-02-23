using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public PlayerViable playerViable; //Not show in Inspector
    [Header("Viable Setting Reference")]
    [Tooltip("Reference to Viable Setting Scriptable Object")]
    public ViableSetting viableSetting;

    public static ChatEventTracker chatContinueTrigger;
    private void Awake()
    {
        InitializePlayer();
        SetupEvent();
    }

    private void ConsumeEnergy(int _amount)
    {
        if(playerViable.playerEnegy < _amount)
        {
            Debug.Log("Not enough energy to consume. Current Energy: " + playerViable.playerEnegy);
            return;
        }
        Debug.Log($"Consume : {_amount} Energys");
        playerViable.playerEnegy -= _amount;
        chatContinueTrigger.InvokeTracked(_amount);
    }
    
    private void SetupEvent()
    {
        chatContinueTrigger = new ChatEventTracker();
        chatContinueTrigger.AddListener(ConsumeEnergy);
    }

    private void InitializePlayer()
    {
        if(viableSetting == null)
        {
            Debug.LogWarning("Viable Setting reference is missing in Player Script.");
            if(GameObject.FindAnyObjectByType<ViableSetting>() != null)
            {
                viableSetting = GameObject.FindAnyObjectByType<ViableSetting>();
                Debug.Log("Viable Setting reference found in the scene and assigned.");
            }
            else
            {
                Debug.LogError("No Viable Setting found in the scene. Please create and assign one.");
                return;
            }
        }
        if (playerViable == null)
        {
            playerViable = new PlayerViable(viableSetting);
            Debug.Log(playerViable.playerName + " has been created." + 
            "\n Energy: " + playerViable.playerEnegy +
            "\n Money: " + playerViable.moneyBalance);
            return;
        }
        Debug.Log("Player Initialized");
    }
}