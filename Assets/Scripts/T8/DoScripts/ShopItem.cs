using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSkin", menuName = "Shop/ItemSkin")]
public class ShopItemSkin : ScriptableObject
{
    public string itemId;
    public string displayName;
    public Material skinMaterial;
    public int price;
}