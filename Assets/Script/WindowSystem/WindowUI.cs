using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class WindowUI : MonoBehaviour , IDragHandler
{
    [Header("Window Icon References")]
    [SerializeField] private Button desktopIcon;
    [Header("Close Button References")]
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform rectTransform;
    [Header("Window Settings")]
    public string windowName;
    public float windowWidth;
    public float windowHeight;

    bool isfirstTimeOpen = false;

    void OnValidate()
    {
        #if UNITY_EDITOR
        this.gameObject.name = windowName;
        rectTransform.sizeDelta = new Vector2(windowWidth, windowHeight);
        #endif
    }

    protected virtual void Start()
    {
        SettUp();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(!InputManager.Instance.IsUsingComputer()) { return; }
        Vector3 grolbalMousePos;
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out grolbalMousePos))
        {
            rectTransform.position = grolbalMousePos;
            ClampToParentBounds();
        }
    }

    //For Setting up the window
    protected virtual void ExecuteWindow()
    {
        Debug.Log("Executing Window: " + windowName);
        if(!isfirstTimeOpen)
        {
            isfirstTimeOpen = true;
        }
        
        this.gameObject.SetActive(true);
    } 
    public virtual void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }

    private void ClampToParentBounds()
    {
        // Calculate the boundaries of the parent
        Vector2 parentSize = WindowManager.instance.sizeReference.rect.size;
        Vector2 elementSize = rectTransform.rect.size;

        // Calculate the half sizes for pivot considerations (assuming center pivot for simplicity)
        // If the pivot is at the center (0.5, 0.5), we use elementSize / 2
        // Adjust these calculations based on the element's actual pivot if necessary
        float halfElementWidth = elementSize.x * rectTransform.pivot.x;
        float halfElementHeight = elementSize.y * rectTransform.pivot.y;

        // Calculate min and max allowed anchored positions
        // Note: anchoredPosition is relative to the parent's anchor points
        float minX = -(parentSize.x / 2) + halfElementWidth;
        float maxX = (parentSize.x / 2) - (elementSize.x - halfElementWidth);
        float minY = -(parentSize.y / 2) + halfElementHeight;
        float maxY = (parentSize.y / 2) - (elementSize.y - halfElementHeight);

        // Clamp the current anchored position
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, minX, maxX);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, minY, maxY);
        rectTransform.anchoredPosition = anchoredPosition;
    }

    protected virtual void SettUp()
    {
        if (closeButton != null && desktopIcon != null)
        {
            closeButton.onClick.AddListener(CloseWindow);
            desktopIcon.onClick.AddListener(ExecuteWindow);
        }
        else
        {
            Debug.LogWarning("Close Button or Desktop Icon is not assigned in " + windowName);
        }

        rectTransform = this.gameObject.GetComponent<RectTransform>();

    }
    

}