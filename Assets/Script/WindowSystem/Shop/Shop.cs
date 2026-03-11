using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : WindowUI
{
    [Header("Resources Setup")]
    [Tooltip("If empty, loads all Item assets located anywhere under Resources.")]
    public string resourcesPath = ""; // e.g. "Shop" or "Shop/Items"
    [Tooltip("Prefab For Item")]
    public ShopItemFrame framePrefab;

    [Header("Parent object for spawn content")]
    public RectTransform contentParent;

    [Header("Button Setup")]
    public Button desktopDeco;
    public Button roomDeco;
    public Button catDeco;
    public Button food;

    [Header("Item Lists")]
    private List<Item> showItem = new List<Item>();
    [Tooltip("Desktop Decoration Items")]
    public List<Item> d_Item = new List<Item>();

    [Tooltip("Room Decoration Items")]
    public List<Item> r_Item = new List<Item>();

    [Tooltip("Cat Decoration Items")]
    public List<Item> c_Item = new List<Item>();

    [Tooltip("Food Items")]
    public List<Item> f_Item = new List<Item>();

    

    protected override void Start()
    {
        base.Start();
        Setup();
    }

    public void Setup()
    {
        if(framePrefab == null)
            framePrefab = Resources.Load<ShopItemFrame>("Prefab/ItemFrame");

        d_Item.Clear();
        r_Item.Clear();
        c_Item.Clear();
        f_Item.Clear();

        Item[] allItems = Resources.LoadAll<Item>(resourcesPath);

        foreach (Item item in allItems)
        {
            switch (item.category)
            {
                case ItemCategory.DesktopDecoration:
                    d_Item.Add(item);
                    break;
                case ItemCategory.RoomDecoration:
                    r_Item.Add(item);
                    break;
                case ItemCategory.CatDecoration:
                    c_Item.Add(item);
                    break;
                case ItemCategory.Food:
                    f_Item.Add(item);
                    break;
                default:
                    break;
            }
        }

        // TODO: Hook up UI (buttons/lists) to display loaded items.
        desktopDeco.onClick.AddListener(delegate ()
        {
            SetShop(d_Item);
        });
    }

    private void SetShop(List<Item> input)
    {
        Vector3 rowTop = new Vector3(-155f,41.5f,0);
        Vector3 rowBot = new Vector3(-155f,-38.5f,0);
        int count = 0;
        foreach(Item item in input)
        {
            if(count % 2 == 0)
            {
                SpawnFrame(item,rowTop);
                rowTop += new Vector3(80,0,0);
                count++;
            }
            else
            {
                SpawnFrame(item,rowBot);
                rowBot += new Vector3(80,0,0);
                count++;
            }
            
        }
    } 

    private void SpawnFrame(Item _item ,Vector3 _position)
    {
        ShopItemFrame frameObj = Instantiate(framePrefab,contentParent);
        RectTransform frameRect = frameObj.GetComponent<RectTransform>();
        frameRect.localPosition = _position + new Vector3(197.7f,-85f,0); //

        frameObj.SetFrame(_item);
    }

}