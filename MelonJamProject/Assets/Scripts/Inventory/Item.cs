using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        DamagePotion,
        IcePotion,
        HealthPotion,
    } 
    
    public ItemType _itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (_itemType)
        {
            default:
            case ItemType.DamagePotion: return ItemAssets.Instance._damagePotionSprite;
            case ItemType.IcePotion: return ItemAssets.Instance._icePotionSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance._healthPotionSprite;
        }
    }
}
