using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatHistory : MonoBehaviour
{
    public Image profileImage;
    public TMP_Text userName;
    public User containUser;
    public RectTransform objRtranform;

    [SerializeField] private Button button;

#if UNITY_EDITOR
    void OnValidate()
    {
        if(profileImage == null || userName == null)
        {
            Debug.LogError("Chat History prefab element is null");
        }
    }
#endif

    void Awake()
    {
        button.onClick.AddListener(delegate ()
        {
            Chat chat = WindowManager.instance.AccessApp(WindowAppType.CatChat).GetComponent<Chat>();
            Debug.Log(chat);
            chat.SetChatHistory(containUser.userID);
        });
    }
    public void SetHistory(User user)
    {
        objRtranform = gameObject.GetComponent<RectTransform>();
        profileImage.sprite = user.profilePicture;
        userName.text = user.userName;
        containUser = user;

        
    }
}