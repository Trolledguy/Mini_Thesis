using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Image blackScreen;
    [SerializeField] private TMP_Text dayText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateDayText(int day)
    {
        dayText.text = "Day " + day;
    }

    public void SetBlackScreen(float duration , bool isOpen)
    {
        StartCoroutine(BlackScreenCoroutine(duration, isOpen));
    }
    private IEnumerator BlackScreenCoroutine(float duration , bool isOpen)
    {
        Debug.Log($"BlackScreenCoroutine started with duration: {duration} and isOpen: {isOpen}");
        switch(isOpen)
        {
            case true:
                blackScreen.gameObject.SetActive(true);
                blackScreen.color = new Color(0f, 0f, 0f, 0f);
                float elapsedTime = 0f;
                yield return new WaitForSeconds(1f); // Optional delay before starting the fade
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float alpha = Mathf.Clamp01(elapsedTime / duration);
                    dayText.color = new Color(dayText.color.r, dayText.color.g, dayText.color.b, 1 - alpha);
                    blackScreen.color = new Color(0f, 0f, 0f, alpha);
                    yield return null;
                }
                blackScreen.color = new Color(0f, 0f, 0f, 1f);
                break;
            case false:
                blackScreen.gameObject.SetActive(true);
                blackScreen.color = new Color(0f, 0f, 0f, 1f);
                elapsedTime = 0f;
                yield return new WaitForSeconds(1f); // Optional delay before starting the fade
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
                    dayText.color = new Color(dayText.color.r, dayText.color.g, dayText.color.b, 1 - alpha);
                    blackScreen.color = new Color(0f, 0f, 0f, alpha);
                    yield return null;
                }
                blackScreen.color = new Color(0f, 0f, 0f, 0f);
                blackScreen.gameObject.SetActive(false);
                break;
        }
    }
}