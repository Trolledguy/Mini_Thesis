using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBlubbleTemplate : MonoBehaviour
{
    public RectTransform rectTransform;
    public TMP_Text messageText;
    public Image imageZone;


    //Size of chat bubble
    private float minimumWidth = 30f;
    private float minimumHight = 20f;
    private float maximumWidth = 115f;
    

    float messageBoxWidth;
    float messageBoxHight;

    private float m_yPos;
    public float yPos
    {
        get
        {
            return m_yPos;
        }
        set
        {
            m_yPos = value;
            float currentX = rectTransform.rect.x;
            rectTransform.position = new Vector2(currentX,value);
        }
    }

    void Awake()
    {
        Setup();
    }

    
    public void SetMessage(string _message)
    {
        messageText.gameObject.SetActive(true);
        imageZone.gameObject.SetActive(false);
        gameObject.SetActive(true);
        messageText.text = _message;
        UpdateMessageboxSize(_message);
    }

    public void SetImage(Sprite sprite)
    {
        imageZone.gameObject.SetActive(true);
        messageText.gameObject.SetActive(false);
        gameObject.SetActive(true);
        imageZone.sprite = sprite;
        UpdateMessageboxSize(sprite);
    }

    private void Setup()
    {
        rectTransform = GetComponent<RectTransform>();
        messageText = GetComponentInChildren<TMP_Text>();
        
        if(imageZone == null) Debug.LogError("No image component assign");

        messageBoxWidth = messageText.rectTransform.rect.width;
        messageBoxHight = messageText.rectTransform.rect.height;

        m_yPos = rectTransform.anchoredPosition.y;

        messageText.gameObject.SetActive(false);
        imageZone.gameObject.SetActive(false);
    }
    private void UpdateMessageboxSize(Sprite _s)
    {
        messageBoxHight = 105;
        messageBoxWidth = 105;
        rectTransform.sizeDelta = new Vector2(messageBoxWidth - 200,messageBoxHight);
    }

    private void UpdateMessageboxSize(string massage)
    {
        //Setup
        messageText.textWrappingMode = TextWrappingModes.Normal;
        messageText.ForceMeshUpdate();

        //Get preferred size with width constraint from the start
        Vector2 preferSize = messageText.GetPreferredValues(massage, maximumWidth, Mathf.Infinity);
        float x = preferSize.x;
        float y = preferSize.y;

        //Apply min/max bounds
        if(x < minimumWidth)
            x = minimumWidth;
        else if(x > maximumWidth)
            x = maximumWidth;
        if(y < minimumHight)
            y = minimumHight;

        //Set Size here
        messageBoxHight = y;
        messageBoxWidth = x;
        rectTransform.sizeDelta = new Vector2(x - 200, y + 2);
    }

    public Vector2 GetBubbleSize()
    {
        return new Vector2(messageBoxWidth,messageBoxHight);
    }


    void OnDrawGizmos()
    { 
        if(rectTransform == null || messageText == null)
        {
            Debug.LogError("RectTranform or Text Missing");
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(messageText.rectTransform.anchoredPosition, new Vector2(messageBoxWidth,messageBoxHight));
    }

}