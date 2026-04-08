using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;
using TMPro;


public class Chat : WindowUI
{
    [Header("Tranform of parent object")]
    [SerializeField] private RectTransform contentTranform;

    [Header("Profile Object")]
    [SerializeField] private Image profileContainer;
    [SerializeField] private Image chatPfp;
    [SerializeField] private TMP_Text chatName;

    [Header("Bubble Prefab")]
    [SerializeField] private GameObject chatblubblePrefab;

    private List<ChatBlubbleTemplate> allBubble = new List<ChatBlubbleTemplate>();

    [SerializeField] private Button debugButton;
    [SerializeField] private Button askNextButton;
    [SerializeField] private string testText;

    [Header("Sound")]
    [SerializeField] private AudioClip chatSound; 
    [SerializeField] private AudioClip chatResetSound;


    private User currentUser;
    private UserChat currentUserChat;
    private Story currentStory;


    private float m_spawnPositionY = 0;



    void Awake()
    {
        this.SettUp();
        if(profileContainer == null || chatPfp == null)
        {
            Debug.LogError("Profile Container not found");
        }
        Debug.Log(chatblubblePrefab);

        contentTranform.sizeDelta = new Vector2(0 , 30);
        profileContainer.gameObject.SetActive(false);
        chatPfp.gameObject.SetActive(false);

        askNextButton.onClick.AddListener(delegate()
        {
            StartCoroutine(ContinueChat());
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

    public IEnumerator ContinueChat(float time = 1f)
    {

        currentStory.ChooseChoiceIndex(0);
        Player.consumeEnergyTrigger.Invoke(10);

        string text = currentStory.Continue();

        AddNewChat(text);

        if(currentStory.canContinue == false)
            yield break;

        while(currentStory.canContinue)
        {
            yield return new WaitForSeconds(time);
            string uText = currentStory.Continue();
            AddNewChat(uText,currentUser);
            profileContainer.gameObject.SetActive(true);
        }
        
    }
    

    public IEnumerator SetNewChat(string _UID , float time = 1f)
    {
        profileContainer.gameObject.SetActive(false);
        chatPfp.gameObject.SetActive(false);
        ClearChat();
        //Get UserChat
        currentUser = UserManager.intensce.GetUserByID(_UID);
        chatName.text = currentUser.userName;
        currentUserChat = currentUser.userChatInfo;
        currentUserChat.SetupChat();
        profileContainer.sprite = currentUser.profilePicture;
        chatPfp.sprite = currentUser.profilePicture;
        chatPfp.gameObject.SetActive(true);
        
        
        currentStory = currentUserChat.userStory;

        if(currentStory.canContinue == false)
            yield break;

        while (currentStory.canContinue)
        {
            yield return new WaitForSeconds(time);
            string uText = currentStory.Continue();
            if(uText == "" || uText == null)
                yield break;
            AddNewChat(uText,currentUser);
            profileContainer.gameObject.SetActive(true);
        }

        
    }
    private IEnumerator SetnewChatHistory(string _UID , float time = 0f)
    {
        profileContainer.gameObject.SetActive(false);
        chatPfp.gameObject.SetActive(false);
        ClearChat();
        //Get UserChat
        User thisUser = UserManager.intensce.GetUserByID(_UID);
        chatName.text = thisUser.userName;
        currentUserChat = thisUser.userChatInfo;
        currentUserChat.SetupChat();
        profileContainer.sprite = thisUser.profilePicture;
        chatPfp.sprite = thisUser.profilePicture;
        chatPfp.gameObject.SetActive(true);
        
        
        currentStory = currentUserChat.userStory;

        if(!currentStory.canContinue)
        {
            while(currentStory.currentChoices.Count > 0)
            {
                currentStory.ChooseChoiceIndex(0);
                string pText = currentStory.Continue();
                AddNewChat(pText);
            }
        }
        while (currentStory.canContinue)
        {
            yield return new WaitForSeconds(time);
            string uText = currentStory.Continue();
            if(uText == "" || uText == null)
            {
                if(currentStory.currentChoices.Count > 0)
                    ContinueHistory(currentStory);
                continue;
            }
                
            AddNewChat(uText,thisUser);
            profileContainer.gameObject.SetActive(true);
            if(currentStory.currentChoices.Count > 0)
                ContinueHistory(currentStory);

        }

        
    }
    private void ContinueHistory(Story story)
    {
        while (story.currentChoices.Count > 0)
        {
            story.ChooseChoiceIndex(0);
            string pText = currentStory.Continue();
            AddNewChat(pText);
        }
    }
    public void ClearChat()
    {
        profileContainer.transform.SetParent(contentTranform);
        profileContainer.gameObject.SetActive(false);
        chatPfp.gameObject.SetActive(false);
        foreach(ChatBlubbleTemplate c in allBubble)
        {
            Destroy(c.gameObject);
        }
        contentTranform.sizeDelta = new Vector2(0 , 30);
        m_spawnPositionY = 0;
        allBubble.Clear();
        currentUser = null;
        currentStory = null;
        this.gameObject.SetActive(true);
        StartCoroutine(Sound.PlaySoundAtPoint(chatResetSound, this.transform.position));
        
    }
    public void SetChatHistory(string uID)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(SetnewChatHistory(uID,0f));   
    }

    private void AddNewChat(string _message , User user = null)
    {  
        if(this.gameObject.activeSelf == false)
            gameObject.SetActive(true);
        //Instantiate chat
        GameObject newBubble = Instantiate(chatblubblePrefab.gameObject, contentTranform, false);
        ChatBlubbleTemplate bubbleInfo = newBubble.GetComponent<ChatBlubbleTemplate>();
        bubbleInfo.SetMessage(_message);
        //SetPosition
        float newBubbleHight = bubbleInfo.GetBubbleSize().y;
            
        float b_expandSize = newBubbleHight + 30 + (newBubbleHight/2);
        ExtentContentZone(b_expandSize);
        
        SetNewChatPosistion(bubbleInfo.rectTransform, user == null);
        m_spawnPositionY += newBubbleHight - (newBubbleHight/2);
        //Add infomation
        allBubble.Add(bubbleInfo);

        StartCoroutine(Sound.PlaySoundAtPoint(chatSound, this.transform.position));


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
        float x = isPlayer ? 17.5f : -17.5f;
        targetRect.anchoredPosition = new Vector2(x, -m_spawnPositionY);

        float hWidth = targetRect.rect.width / 2;
        float hHight = targetRect.rect.height / 2;
        if(isPlayer) return;
        profileContainer.transform.SetParent(targetRect);
        profileContainer.transform.localPosition = new Vector2((-hWidth) - 10, (-hHight) + 7.5f );
    }


}