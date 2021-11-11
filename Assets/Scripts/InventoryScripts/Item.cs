using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
    public enum ItemType
    {
        None,
        MetalScrap,
        PolymerScrap,
        SiliconScrap,
        RefinedMetal,
        RefinedPolymer,
        RefinedSilicon,
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
            case ItemType.MetalScrap: return ItemMesh.Instance.cubeMesh;
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
            case ItemType.MetalScrap: 
            case ItemType.PolymerScrap: 
            case ItemType.SiliconScrap:      
                return true;

                return false;
        }
    }

    //public int GetCost()
    //{
    //    return GetCost(itemType);
    //}

    //public static int GetCost(ItemType itemType)
    //{
    //    switch (itemType)
    //    {
    //        default:
   
    //    }
    //}

    public override string ToString()
    {
        return itemType.ToString();
    }
}
