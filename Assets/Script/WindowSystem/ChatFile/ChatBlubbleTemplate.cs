using TMPro;
using UnityEngine;

public class ChatBlubbleTemplate : MonoBehaviour
{
    public RectTransform rectTransform;
    public TMP_Text messageText;


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
        messageText.text = _message;
        UpdateMessageboxSize(_message);
    }

    private void Setup()
    {
        rectTransform = GetComponent<RectTransform>();
        messageText = GetComponentInChildren<TMP_Text>();

        messageBoxWidth = messageText.rectTransform.rect.width;
        messageBoxHight = messageText.rectTransform.rect.height;

        m_yPos = rectTransform.anchoredPosition.y;
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