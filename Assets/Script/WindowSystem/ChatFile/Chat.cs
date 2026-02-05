using UnityEngine;

public class Chat : WindowUI
{
    private GameObject chatblubblePrefab;
    protected override void Start()
    {
        this.SettUp();
        chatblubblePrefab = Resources.Load<GameObject>("Prefab/Chat_Bubble_Template");
    }

}