using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        DamagePotion,
    } 
    
    public ItemType _itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (_itemType)
        {
            default:
            case ItemType.DamagePotion: return ItemAssets.Instance._damagePotionSprite;
        }
    }
}
