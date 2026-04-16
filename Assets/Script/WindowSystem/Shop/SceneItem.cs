using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/SceneItem", order = 100)]
public class SceneItem : Item
{
    public SceneSkinInfo skinInfo;
    public override void UseItem()
    {
        SkinManager.intence.ToggleChangeScene(skinInfo);
    }
}