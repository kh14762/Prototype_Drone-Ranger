using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_RefSlot : MonoBehaviour, IDropHandler
{
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    public class OnItemDroppedEventArgs : EventArgs
    {
        public Item item;
        public int amount;
    }

    public void OnDrop(PointerEventData eventData)
    {
        UI_ItemDrag.Instance.Hide();
        Item item = UI_ItemDrag.Instance.GetItem();
        OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs { item = item, amount = item.amount });
    }

}
