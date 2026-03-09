using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : WindowUI
{
    [Header("Resources Setup")]
    [Tooltip("If empty, loads all Item assets located anywhere under Resources.")]
    public string resourcesPath = ""; // e.g. "Shop" or "Shop/Items"

    [Header("Button Setup")]
    public Button deskTopDeco;
    public Button roomDeco;
    public Button catDeco;
    public Button food;

    [Header("Item Lists")]
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
    }
}