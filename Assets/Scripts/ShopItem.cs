using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ShopItem/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int price;
}
