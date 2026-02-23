using TMPro;
using UnityEngine;

public class DebugBox : MonoBehaviour
{
    public static DebugBox instance; 
    [SerializeField] private TMP_Text debugText;

    void Awake()
    {
        instance = this;
    }

    public void AddDebugText(string text)
    {
        debugText.text += text + "\n";
    }
}