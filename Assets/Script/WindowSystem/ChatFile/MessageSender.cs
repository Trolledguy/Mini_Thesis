using UnityEngine;
using UnityEngine.Events;

public class MessageSender : MonoBehaviour
{
    public UnityEvent<string> onCallEvent;

    void Start()
    {
        onCallEvent.AddListener(SendID);
    }

    public void SendID(string _ID)
    {
        Chat chat = WindowManager.instance.AccessChat();
        StartCoroutine(chat.SetNewChat(_ID));
        Debug.Log("Receive user ID : " + _ID);
    }
}