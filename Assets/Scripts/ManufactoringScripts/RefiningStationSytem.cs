using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefiningStationSystem
{
    Item item;
    public RefiningStationSystem()
    {
        item = new Item();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player colliding with Refining Station");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player no longer with Refining Station");
        }
    }

    private bool IsEmpty()
    {
        // return true if null else return false
        return item == null;
    }

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void IncreaseItemAmount()
    {
        GetItem().amount++;
    }

    public void DecreaseItemAmount()
    {
        GetItem().amount--;
    }

    public void RemoveItem()
    {
        SetItem(null);
    } 

    public bool TryAddItem(Item item)
    {
        if (IsEmpty())
        {
            SetItem(item);
            return true;
        } else
        {
            if (item.itemType == GetItem().itemType)
            {
                IncreaseItemAmount();
                return true;
            } else
            {
                return false;
            }
        }
    }
}
