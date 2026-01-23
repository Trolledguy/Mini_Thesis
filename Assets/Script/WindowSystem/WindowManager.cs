using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    public RectTransform sizeReference;
    public Canvas windowCanvas;
    public float screenWidth;
    public float screenHeight;

    void Start()
    {
        Setup();
        screenWidth = sizeReference.sizeDelta.x;
        screenHeight = sizeReference.sizeDelta.y;
    }

    private void Setup()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sizeReference = GetComponent<RectTransform>();
        windowCanvas = GetComponentInParent<Canvas>();

    }
}