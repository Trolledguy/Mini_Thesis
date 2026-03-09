using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class Shop : WindowUI
{
    [Header("Button Setup")]
    public Button deskTopDeco;
    public Button roomDeco;
    public Button catDeco;
    public Button food;

    [Header("ItemList")]
    [Tooltip("Desktop Decoration Item")]
    public List<Item> d_Item = new List<Item>();
    [Tooltip("Room Decoration Item")]
    public List<Item> r_Item = new List<Item>();
    [Tooltip("Cat Decoration Item")]
    public List<Item> c_Item = new List<Item>();
    [Tooltip("Food Item")]
    public List<Item> f_Item = new List<Item>();

    void Start()
    {
        Setup();   
    }

    private void Setup()
    {
        Item[] dDeco = Resources.LoadAll<Item>(""); //Add Path Later
        foreach(Item item in dDeco)
        {
            d_Item.Add(item);
        }
    }
}