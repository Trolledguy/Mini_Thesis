using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using Ink.Runtime;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Chat : WindowUI
{
    [Header("Tranform of parent object")]
    [SerializeField]
    private RectTransform contentTranform;
    private GameObject chatblubblePrefab;
    private List<ChatBlubbleTemplate> allBubble = new List<ChatBlubbleTemplate>();

    [SerializeField] private Button debugButton;
    [SerializeField] private Button askNextButton;
    [SerializeField] private string testText;


    private UserChat currentUserChat;

    private float m_spawnPositionY = 0;


    List<string> lastTags;


    protected override void Start()
    {
        this.SettUp();
        chatblubblePrefab = Resources.Load<GameObject>("Prefab/Chat_Bubble_Template");

        contentTranform.sizeDelta = new Vector2(0 , 30);

        askNextButton.onClick.AddListener(delegate()
        {
            Player.chatContinueTrigger.Invoke(10);
        });


        //
        debugButton.onClick.AddListener(delegate()
        {
            AddNewChat(testText);
        });
    }


    /// <summary>
    /// User Chat Handler
    /// </summary>

    public IEnumerator SetNewChat(string _UID)
    {
        Debug.Log("Setting new Chat");
        foreach(ChatBlubbleTemplate c in allBubble) //Clear all bubble
        {
            Destroy(c.gameObject);
        }
        contentTranform.sizeDelta = new Vector2(0 , 30);
        m_spawnPositionY = 0;
        allBubble.Clear();

        //Get UserChat
        User u = UserManager.intensce.GetUserByID(_UID);
        currentUserChat = u.userChatInfo;
        currentUserChat.SetupChat();
        
        Story s = currentUserChat.userStory;

        
        while (s.canContinue)
        {
            yield return new WaitForSeconds(1f);
            string text = s.Continue();
            List<string>tags = s.currentTags;

            
            //Check if is image post
            if(UserChat.IsTagChange(lastTags,tags) && tags.Count > 0)
            {
                Debug.Log("Tag Change");
                lastTags = tags;
                currentUserChat.HandleTags(lastTags);
                AddNewImage(u);
                
                continue;
            }

            AddNewChat(text,u);
            yield return null;
            
        }
    }
    private void AddNewImage(User user = null)
    {  
        //Setup image
        Sprite spriteImg = user.userChatInfo.GetCurrentImage();
        //Instantiate chat
        GameObject newBubble = Instantiate(chatblubblePrefab, contentTranform, false);
        ChatBlubbleTemplate bubbleInfo = newBubble.GetComponent<ChatBlubbleTemplate>();
        Button button = bubbleInfo.AddComponent<Button>();
        button.onClick.AddListener(delegate()
        {
            Debug.Log("Image Clicked");
            CatProfile catProfile = WindowManager.instance.catProfilePrefab;
            GameObject obj = Instantiate(catProfile.gameObject);
            obj.transform.SetParent(WindowManager.instance.windowCanvas.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            catProfile = obj.GetComponent<CatProfile>();
            catProfile.SetCatProfile(user.userRequestedCat, user);
        });
        bubbleInfo.SetImage(spriteImg);
        //SetPosition
        float newBubbleHight = bubbleInfo.GetBubbleSize().y;
            
        float b_expandSize = newBubbleHight + (newBubbleHight/3);
        ExtentContentZone(b_expandSize);
        
        SetNewChatPosistion(bubbleInfo.rectTransform, user == null);
        m_spawnPositionY += newBubbleHight - (newBubbleHight/2);
        //Add infomation
        allBubble.Add(bubbleInfo);

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
        


    }


    private void ExtentContentZone(float amount)
    {
        // Cache world positions
        List<Vector3> oldWorldPos = GetWorldBubblesPos();

        // Resize content
        contentTranform.sizeDelta += new Vector2(0, amount);

        // Restore positions so nothing shifts on screen
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