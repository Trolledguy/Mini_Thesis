using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerViable playerViable; //Not show in Inspector
    [Header("Viable Setting Reference")]
    [Tooltip("Reference to Viable Setting Scriptable Object")]
    public ViableSetting viableSetting;
    private void Awake()
    {
        InitializePlayer();
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