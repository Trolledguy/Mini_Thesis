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
    private void ClearBox()
    {
        instance.debugText.text = "";
    }
    public static void UpdateScore(int amount)
    {
        instance.scoreText.text = "Score : " + amount.ToString();
    }

    public static void AddDebugText(string text)
    {
        instance.ClearBox();
        instance.debugText.text += text + "\n";
    }
}