using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBlubbleTemplate : MonoBehaviour
{
    public RectTransform rectTransform;
    public TMP_Text messageText;

    [SerializeField] private Button debugButton;
    [SerializeField] private string testText;
    //
    private float minimumWidth = 30f;
    private float minimumHight = 20f;
    private float maximumWidth = 115f;
    

    float messageBoxWidth;
    float messageBoxHight;

    void Awake()
    {
        Setup();
        debugButton.onClick.AddListener(delegate()
        {
            SetMessage(testText);
        });
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
        rectTransform.sizeDelta = new Vector2(x - 200, y);
    }
    private void SetBond()
    {
        

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