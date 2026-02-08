using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Chat : WindowUI
{
    [Header("Tranform of parent object")]
    [SerializeField]
    private RectTransform contentTranform;
    private GameObject chatblubblePrefab;
    private List<ChatBlubbleTemplate> allBubble = new List<ChatBlubbleTemplate>();

    [SerializeField] private Button debugButton;
    [SerializeField] private string testText;


    private UserChat currentUserChat;

    private float m_spawnPositionY = 0;


    protected override void Start()
    {
        this.SettUp();
        chatblubblePrefab = Resources.Load<GameObject>("Prefab/Chat_Bubble_Template");

        contentTranform.sizeDelta = new Vector2(0 , 30);


        //
        debugButton.onClick.AddListener(delegate()
        {
            AddNewChat(testText);
        });
    }


    /// <summary>
    /// User Chat Handler
    /// </summary>

    public void SetNewChat(string _UID)
    {
        Debug.Log("Setting new Chat");
        User u = UserManager.intensce.GetUserByID(_UID);
        currentUserChat = u.userChatInfo;
        while (currentUserChat.userStory.canContinue)
        {
            string text = currentUserChat.userStory.Continue();
            AddNewChat(text,u);
            Debug.Log($"Log Chat {text}");
        }
        Debug.Log("End Setup");
    }

    private void AddNewChat(string _message , User user = null)
    {  
        //Instantiate chat
        GameObject newBubble = Instantiate(chatblubblePrefab, contentTranform, false);
        ChatBlubbleTemplate bubbleInfo = newBubble.GetComponent<ChatBlubbleTemplate>();
        bubbleInfo.SetMessage(_message);
        //SetPosition
        float newBubbleHight = bubbleInfo.GetBubbleSize().y;
            
        float b_expandSize = newBubbleHight + (newBubbleHight/3);
        ExtentContentZone(b_expandSize);
        
        SetNewChatPosistion(bubbleInfo.rectTransform, user == null);
        m_spawnPositionY += newBubbleHight - (newBubbleHight/2);
        //Add infomation
        allBubble.Add(bubbleInfo);
        
        
        if(user != null)
        {
            //SetNewChatPosistion(bubbleInfo.rectTransform, user);
            Debug.Log("This is User Chat"); // Posx = -17.5
        }
        else
        {
            
            Debug.Log("This chat is player chat"); //Posx = 17.5
        }


    }


    private void ExtentContentZone(float amount)
    {
        // 1. Cache world positions
        List<Vector3> oldWorldPos = GetWorldBubblesPos();

        // 2. Resize content
        contentTranform.sizeDelta += new Vector2(0, amount);

        // 3. Restore positions so nothing shifts on screen
        RestoreWorldBubblesPos(oldWorldPos);
    }

    private List<Vector3> GetWorldBubblesPos()
    {
        List<Vector3> posList = new List<Vector3>();
        foreach (ChatBlubbleTemplate bubble in allBubble)
        {
            posList.Add(bubble.rectTransform.position);
        }
        return posList;
    }

    private void RestoreWorldBubblesPos(List<Vector3> worldPos)
    {
        for (int i = 0; i < allBubble.Count; i++)
        {
            allBubble[i].rectTransform.position = worldPos[i];
        }
    }

    private void SetNewChatPosistion(RectTransform targetRect ,bool isPlayer)
    {
        if (isPlayer)
        {
            targetRect.anchoredPosition = new Vector2(17.5f, - m_spawnPositionY);
        }
        else
        {
            targetRect.anchoredPosition = new Vector2(-30.5f, - m_spawnPositionY);
        }
    }

}