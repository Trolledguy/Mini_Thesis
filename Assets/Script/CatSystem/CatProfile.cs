using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.PackageManager.UI;


public class CatProfile : MonoBehaviour
{
    [SerializeField] private TMP_Text catNameText;
    [SerializeField] private TMP_Text breedText;
    [SerializeField] private TMP_Text ageText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image catImageRenderer;

    [SerializeField] private Button approveButton;
    [SerializeField] private Button denyButton;

    private User currentUser;

    void Awake()
    {
        gameObject.SetActive(false);
        approveButton.onClick.AddListener(() => OnApprove(currentUser));
        denyButton.onClick.AddListener(() => OnDeny(currentUser));
    }

    private void OnApprove(User user)
    {
        Debug.Log("Approved cat : " + catNameText.text);
        GameManager.Instance.UpdateScore(user.isLoveAnimals);
        WindowManager.instance.AccessCatbook().UpdateFeed(PostStatus.Scrollable); // Set to null for now
        WindowManager.instance.AccessChat().ClearChat();
        Player.onDesignEvent.Invoke(); // Trigger the design event to consume energy
        Destroy(gameObject);
    }
    private void OnDeny(User user)
    {
        Debug.Log("Denied cat : " + catNameText.text);
        GameManager.Instance.UpdateScore(!user.isLoveAnimals);
        WindowManager.instance.AccessCatbook().UpdateFeed(PostStatus.Scrollable); // Set to null for now
        WindowManager.instance.AccessChat().ClearChat();
        Player.onDesignEvent.Invoke(); 
        Destroy(gameObject);
    }

    public void SetCatProfile(CatInfo catInfo , User user)
    {
        catNameText.text = catInfo.catName;
        breedText.text = catInfo.breed;
        ageText.text = $"Age: {catInfo.age}";
        descriptionText.text = catInfo.description;
        catImageRenderer.sprite = catInfo.catImage;
        currentUser = user;
        this.gameObject.SetActive(true);
    }
}