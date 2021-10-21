using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefiningSystem : MonoBehaviour, IItemHolder
{
    private Item item;

    public event EventHandler OnChange;
    [SerializeField] private UI_RefiningSystem uiRefiningSystem;

    void Start()
    {
        uiRefiningSystem.SetRefiningSystem(this);
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

    public bool IsEmpty()
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
        if (item != null)
        {
            item.RemoveFromItemHolder();
            item.SetItemHolder(this);
        }
        this.item = item;
        OnChange?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseItemAmount()
    {
        GetItem().amount++;
        OnChange?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseItemAmount()
    {
        GetItem().amount--;
        OnChange?.Invoke(this, EventArgs.Empty);
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

    public void RemoveItem(Item item)
    {
        throw new NotImplementedException();
    }

    public void AddItem(Item item)
    {
        throw new NotImplementedException();
    }

    public bool CanAddItem()
    {
        throw new NotImplementedException();
    }
}
