using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerViable playerViable;
    private void Awake()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        if (playerViable == null)
        {
            Debug.LogWarning("PlayerViable component is not assigned.");
            return;
        }
        Debug.Log("Player Initialized");
    }
}