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
        Chat chat = WindowManager.instance.AccessApp(WindowAppType.CatChat).GetComponent<Chat>();
        
        GameManager.Instance.currentUserID = _ID; //Update Current User
        chat.gameObject.SetActive(true);
        StartCoroutine(chat.SetNewChat(_ID));
        
    }
}