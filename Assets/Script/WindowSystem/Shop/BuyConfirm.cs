using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyConfirm : MonoBehaviour
{
    [Header("Shop iteminfo display")]
    public Image image;
    public TMP_Text priceText;
    
    [Header("Design Button")]
    public Button approve;
    public Button deny;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Set(Item item)
    {
        image.sprite = item.icon;
        priceText.text = item.value.ToString();

        gameObject.SetActive(true);

        approve.onClick.RemoveAllListeners();
        deny.onClick.RemoveAllListeners();
        approve.onClick.AddListener(delegate ()
        {
            Shop.buyEvent.Invoke();
            Debug.Log("Approve Pressed");
            gameObject.SetActive(false);
        });

        deny.onClick.AddListener(delegate ()
        {
            gameObject.SetActive(false);
        });
    }
}