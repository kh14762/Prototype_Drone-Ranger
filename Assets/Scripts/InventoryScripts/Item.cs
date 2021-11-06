using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
        ManaPotion,
        Coin,
        Medkit,
        Cube,
        MetalScrap,
        PolymerScrap,
        SiliconScrap,
    }

    public ItemType itemType;
    public int amount;
    private IItemHolder itemHolder;

    public void SetItemHolder(IItemHolder itemHolder)
    {
        this.itemHolder = itemHolder;
    }

    public IItemHolder GetItemHolder()
    {
        return itemHolder;
    }

    public void RemoveFromItemHolder()
    {
        if (itemHolder != null)
        {
            // Remove from current Item Holder
            itemHolder.RemoveItem(this);
        }
    }

    public void MoveToAnotherItemHolder(IItemHolder newItemHolder)
    {
        RemoveFromItemHolder();
        // Add to new Item Holder
        newItemHolder.AddItem(this);
    }

    public Sprite GetSprite()
    {
        return GetSprite(itemType);
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:            return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion:     return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:       return ItemAssets.Instance.manaPotionSprite;
            case ItemType.Coin:             return ItemAssets.Instance.coinSprite;
            case ItemType.Medkit:           return ItemAssets.Instance.medkitSprite;
            case ItemType.Cube:             return ItemAssets.Instance.cubeSprite;
            case ItemType.MetalScrap:       return ItemAssets.Instance.MetalScrapSprite;
            case ItemType.PolymerScrap:     return ItemAssets.Instance.PolymerScrapSprite;
            case ItemType.SiliconScrap:     return ItemAssets.Instance.SiliconScrapSprite;
        }
    }

    public Mesh GetMesh()
    {
        switch (itemType)
        {
            default:    
            case ItemType.Cube: return ItemMesh.Instance.cubeMesh;
        }
    }
    public bool IsStackable()
    {
        return IsStackable(itemType);
    }

    public bool IsStackable(ItemType itemType)
    {
        switch(itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
            case ItemType.Cube:
            case ItemType.MetalScrap: 
            case ItemType.PolymerScrap: 
            case ItemType.SiliconScrap:      
                return true;
            case ItemType.Sword:
            case ItemType.Medkit:
                return false;
        }
    }

    public int GetCost()
    {
        return GetCost(itemType);
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return 30;
   
        }
    }

    public override string ToString()
    {
        return itemType.ToString();
    }
}
