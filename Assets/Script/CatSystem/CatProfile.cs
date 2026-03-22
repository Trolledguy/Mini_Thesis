using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class CatProfile : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private TMP_Text catNameText;
    [SerializeField] private TMP_Text breedText;
    [SerializeField] private TMP_Text ageText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image catImageRenderer;

    [SerializeField] private Button approveButton;
    [SerializeField] private Button denyButton;
    private Vector3 spawnPosition;
    private Vector3 spawnRotation;

    public Image bg;
    private Cat currentCat;

    

    private void OnDesign(User user)
    {
        Cat cat = GameManager.Instance.selectCat;
        Debug.Log(cat);
        Debug.Log(user);
        GameManager.Instance.UpdateScore(cat , user);
        Catbook catbook = WindowManager.instance.AccessApp(WindowAppType.Catbook).GetComponent<Catbook>();
        Chat chat = WindowManager.instance.AccessApp(WindowAppType.CatChat).GetComponent<Chat>();
        catbook.UpdateFeed(PostStatus.Scrollable);
        chat.ClearChat(); 
        //TODO : Add past Chat
        Player.onDesignEvent.Invoke(); // Trigger the design event to consume energy
        Destroy(gameObject);
        InputManager.Instance.selectCatProfile = null;
        InputManager.Instance.isInspecting = false;
    }


    public void ChangeSkin(UISetInfo skinInfo)
    {
        bg.sprite = skinInfo.catProfileBg;
        approveButton.image.sprite = skinInfo.appProveButton;
        denyButton.image.sprite = skinInfo.denyButton;
    }

    public void SetCatProfile(CatInfo catInfo, Vector2 position , float zRotation)
    {
        gameObject.SetActive(false);
        SetOrigin(position, zRotation);
        ResetPosition();

        currentCat = CatManager.instance.GetCatByID(catInfo.catID);
        catNameText.text = catInfo.catName; 
        breedText.text = catInfo.breed;
        ageText.text = $"Age: {catInfo.age}";
        descriptionText.text = catInfo.description;
        catImageRenderer.sprite = catInfo.catImage;
        this.gameObject.SetActive(true);
    }
    private void SetOrigin(Vector2 pos,float zRotation)
    {
        spawnPosition = pos;
        spawnRotation = new Vector3(0,0,zRotation);
    }

    public void ResetPosition()
    {
        transform.localPosition = spawnPosition;
        transform.localEulerAngles = spawnRotation;
        transform.localScale = Vector3.one * 0.5f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        try
        {
        string uID = GameManager.Instance.currentUserID;
        User cUser = UserManager.intensce.GetUserByID(uID);
        approveButton.onClick.AddListener(() => OnDesign(cUser));
        denyButton.onClick.AddListener(() => OnDesign(cUser));
        }
        catch(NullReferenceException) { Debug.Log("No Current User"); }

        transform.position = PlayerCamera.Instance.playerCamera.transform.position + new Vector3(-0.5f,0,0);
        transform.localScale = Vector3.one;
        transform.LookAt(PlayerCamera.Instance.playerCamera.transform);
        RectTransform rectT = GetComponent<RectTransform>();
        rectT.SetAsLastSibling();
        GameManager.Instance.selectCat = CatManager.instance.GetCatByID(currentCat.catInfo.catID);
        
        InputManager.Instance.selectCatProfile = this;
        InputManager.Instance.isInspecting = true;
    }
}