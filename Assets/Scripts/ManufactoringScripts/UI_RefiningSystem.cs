using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RefiningSystem : MonoBehaviour
{
    [SerializeField] public Transform pfIU_Item;
    private Transform inputSlot;
    private Transform outputSlotTransform;
    private Transform itemContainer;


    private void Awake()
    {
        inputSlot = transform.Find("InputSlot");
        outputSlotTransform = transform.Find("OutputSlot");
        itemContainer = transform.Find("ItemContainer");

        CreateItemInput(new Item { itemType = Item.ItemType.Cube });
        
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
