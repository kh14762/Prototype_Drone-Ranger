using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RefiningSystem : MonoBehaviour
{
    [SerializeField] public Transform pfIU_Item;
    private Transform inputSlot;
    private Transform outputSlotTransform;
    private Transform itemContainer;


    void Start()
    {
        inputSlot = transform.Find("InputSlot");
        outputSlotTransform = transform.Find("OutputSlot");
        itemContainer = transform.Find("ItemContainer");

        //  Subscrib to on item dropped event
        UI_RefiningStationSlot refiningStationSlot = inputSlot.GetComponent<UI_RefiningStationSlot>();
        refiningStationSlot.OnItemDropped += UI_RefiningSystem_OnItemDropped;

        //CreateItemInput(new Item { itemType = Item.ItemType.Cube });
        //CreateItemOutput(new Item { itemType = Item.ItemType.MetalScrap });
    }

    private void UI_RefiningSystem_OnItemDropped(object sender, UI_RefiningStationSlot.OnItemDroppedEventArgs e)
    {
        Debug.Log(e.item); 
    }

    private void CreateItemInput(Item item)
    {
        Transform uiItemTransform = Instantiate(pfIU_Item, itemContainer);
        RectTransform itemRectTransform = uiItemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = inputSlot.GetComponent<RectTransform>().anchoredPosition;
        uiItemTransform.GetComponent<UI_Item>().SetItem(item);
    }

    private void CreateItemOutput(Item item)
    {
        Transform itemTransform = Instantiate(pfIU_Item, itemContainer);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = outputSlotTransform.GetComponent<RectTransform>().anchoredPosition;
        itemTransform.GetComponent<UI_Item>().SetItem(item);
    }
}
