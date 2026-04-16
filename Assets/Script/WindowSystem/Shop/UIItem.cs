using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/UIItem", order = 100)]
public class UIItem : Item
{
    public UISetInfo uISet;
    public override void UseItem()
    {
        string uiID = uISet.id;
        SkinManager.intence.ChangeUI(uiID);
    }
}
