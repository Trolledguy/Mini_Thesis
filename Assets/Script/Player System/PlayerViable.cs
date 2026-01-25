using UnityEngine;

public class PlayerViable
{
    [Header("Player Settings")]
    public string playerName;
    protected float playerEnergy;
    protected int moneyBalance;

    public PlayerViable()
    {
        playerName = "DefaultPlayer";
        playerEnergy = 100.0f;
        moneyBalance = 0;
    }
} 