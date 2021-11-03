using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Printer : MonoBehaviour
{
    [SerializeField] private Transform pfUI_Item;
    private Transform inputSlotTransform;
    private Transform outputSlotTransform;
    private Transform itemContainer;
    private RefiningSystem refiningSystem;
    private UI_RefSlot uiRefSlot;
    private CanvasGroup canvasGroup;

    void Start()
    {
        
    }

    private void CreateItemInput(Item item)
    {
        //Transform uiItemTransform = Instantiate(pfIU_Item, itemContainer);
        //RectTransform itemRectTransform = uiItemTransform.GetComponent<RectTransform>();
        //itemRectTransform.anchoredPosition = inputSlotTransform.GetComponent<RectTransform>().anchoredPosition;
        //uiItemTransform.GetComponent<UI_Item>().SetItem(item);

        //UI_Item uiItem = uiItemTransform.GetComponent<UI_Item>();

    }
}
