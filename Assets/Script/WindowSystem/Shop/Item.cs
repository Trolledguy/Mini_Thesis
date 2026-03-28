using UnityEngine;


public abstract class Item : ScriptableObject
{
    [Tooltip("Display name shown in the shop.")]
    public string itemName;

    [Tooltip("Cost or value of the item.")]
    public int value;

    [Tooltip("Category used by the shop to group items.")]
    public ItemCategory category = ItemCategory.DesktopDecoration;

    [Tooltip("Icon / preview image for the item.")]
    public Sprite icon;

    public bool isbuy = false;

#if UNITY_EDITOR
    void OnValidate()
    {
        if(!Application.isPlaying)
           isbuy = false;   
    }
#endif
}
