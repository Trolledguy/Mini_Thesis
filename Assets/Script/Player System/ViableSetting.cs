using UnityEngine;


public class ViableSetting : MonoBehaviour
{
    [Header("Custom and Define Player viable")]
    public int playerEnergy = 10;
    public int moneyBalance = 0;
    public float timeRemainingPerDay; //in seconds

    [Header("Game Balance Settings")]
    public int energyCostPerChatContinue;
    public int[] daysFeedRequired; // Array to define how many feeds are required for each day progression

}