using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using TMPro;

public class Player : MonoBehaviour
{
    public PlayerViable playerViable; //Not show in Inspector
    [Header("Viable Setting Reference")]
    [Tooltip("Reference to Viable Setting Scriptable Object")]
    public ViableSetting viableSetting;
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;
    [Header("Viable visualize")]
    public TMP_Text coin;
    public TMP_Text enegy;


    public static ChatEventTracker consumeEnergyTrigger;
    public static UnityEvent onDesignEvent = new UnityEvent();
    private void Awake()
    {
        InitializePlayer();
        SetupEvent();
    }
    public void SetEnegy(int index)
    {
        playerViable.playerEnegy = index;
        enegy.text = index.ToString();
    }

    private void ConsumeEnergy(int _amount)
    {
        if(playerViable.playerEnegy < _amount)
        {
            Debug.Log("Not enough energy to consume. Current Energy: " + playerViable.playerEnegy);
            return;
        }
        playerViable.playerEnegy -= _amount;
        enegy.text = playerViable.playerEnegy.ToString();
        consumeEnergyTrigger.InvokeTracked(_amount);
    }
    
    private void SetupEvent()
    {
        consumeEnergyTrigger = new ChatEventTracker();
        consumeEnergyTrigger.AddListener(
            delegate(int energyCost) 
            {
                ConsumeEnergy(energyCost);
                
            }
        );
        onDesignEvent.AddListener(delegate() 
        {
            ConsumeEnergy(viableSetting.energyCostPerChatContinue);
        });
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
        }
        enegy.text = playerViable.playerEnegy.ToString();
        Debug.Log("Player Initialized");
    }
}