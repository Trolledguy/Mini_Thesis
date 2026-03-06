using TMPro;
using UnityEditor.PackageManager.UI;
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
        messageBoxHight = 110;
        messageBoxWidth = 110;
        rectTransform.sizeDelta = new Vector2(messageBoxWidth - 173.14f,messageBoxHight);
    }

    private void UpdateMessageboxSize(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        // Ensure text wraps and the mesh is up to date before measuring
        messageText.textWrappingMode = TextWrappingModes.Normal;
        messageText.overflowMode = TextOverflowModes.Overflow;
        messageText.ForceMeshUpdate();

        // Get preferred size with a max width constraint
        Vector2 preferredSize = messageText.GetPreferredValues(message, maximumWidth, Mathf.Infinity);
        float textWidth = Mathf.Clamp(preferredSize.x, minimumWidth, maximumWidth);
        float textHeight = Mathf.Max(preferredSize.y, minimumHight);

        // Add padding so the bubble doesn't clip the text
        const float paddingHorizontal = 16f;
        const float paddingVertical = 10f;

        // Update stored size
        if(textWidth < minimumWidth)
            textWidth = minimumWidth;
        else if(textWidth > maximumWidth)
            textWidth = maximumWidth;
        if(textHeight < minimumHight)
            textHeight = minimumHight;
        messageBoxWidth = textWidth;
        messageBoxHight = textHeight;

        // Set overall bubble size
        rectTransform.sizeDelta = new Vector2(textWidth + paddingHorizontal - 180f, textHeight + paddingVertical);
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