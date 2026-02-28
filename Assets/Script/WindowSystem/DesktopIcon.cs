using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DesktopIcon : Button , IDragHandler
{
    private ButtonInfo info;
    private RectTransform rectTransform;

    public void OnDrag(PointerEventData eventData)
    {
        if(!InputManager.Instance.IsUsingComputer()) { return; }
        Vector3 globalMousePos;
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            transform.position = globalMousePos;
            ClampToParentBounds();
        }
    }

    protected override void Start()
    {
        base.Start();
        if(info == null)
        {
            info = GetComponent<ButtonInfo>();
        }
        else
        {
            Debug.LogError("No ButtonInfo component found on " + gameObject.name);
        }

        rectTransform = GetComponent<RectTransform>();
        this.onClick.AddListener(OpenLinkedWindow);
    }

    private void OpenLinkedWindow()
    {
        if(info != null && info.linkedWindow != null)
        {
            info.linkedWindow.ExecuteWindow();
        }
        else
        {
            Debug.LogError("No linked window assigned to " + gameObject.name);
        }
    }
        private void ClampToParentBounds()
    {
        // Calculate the boundaries of the parent
        Vector2 parentSize = WindowManager.instance.sizeReference.rect.size;
        Vector2 elementSize = rectTransform.rect.size * rectTransform.lossyScale; // Consider the scale of the element

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

}

