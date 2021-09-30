using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
        ManaPotion,
        Coin,
        Medkit,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:            return ItemSprites.Instance.swordSprite;
            case ItemType.HealthPotion:     return ItemSprites.Instance.healthPotionSprite;
            case ItemType.ManaPotion:       return ItemSprites.Instance.manaPotionSprite;
            case ItemType.Coin:             return ItemSprites.Instance.coinSprite;
            case ItemType.Medkit:           return ItemSprites.Instance.medkitSprite;
        }
    }
}
