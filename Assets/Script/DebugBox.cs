using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DebugBox : MonoBehaviour
{
    public static DebugBox instance; 
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private TMP_Text scoreText;

    void Awake()
    {
        instance = this;
    }

    public static void UpdateScore(int amount)
    {
        instance.scoreText.text = amount.ToString();
    }

    public static void AddDebugText(string text)
    {
        instance.debugText.text += text + "\n";
    }
}