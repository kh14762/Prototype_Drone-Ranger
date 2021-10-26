using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RefiningSystem : MonoBehaviour
{
    [SerializeField] private Transform pfIU_Item;
    private Transform inputSlotTransform;
    private Transform outputSlotTransform;
    private Transform itemContainer;
    private RefiningSystem refiningSystem;
    private UI_RefSlot uiRefSlot;
    private CanvasGroup canvasGroup;


    void Start()
    {
        inputSlotTransform = transform.Find("InputSlot");
        outputSlotTransform = transform.Find("OutputSlot");
        itemContainer = transform.Find("ItemContainer");
        uiRefSlot = inputSlotTransform.GetComponent<UI_RefSlot>();

        // Subscribe to OnItemDropped Event for inputSlot
        uiRefSlot.OnItemDropped += UI_RefiningSystem_OnItemDropped;

        //  Set canvas goup var so this UI component can be Toggled
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

    }

    public void SetRefiningSystem(RefiningSystem refiningSystem)
    {
        this.refiningSystem = refiningSystem;
        refiningSystem.OnChange += RefiningSystem_OnChange;
        UpdateUI();
    }

    private void RefiningSystem_OnChange(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void UI_RefiningSystem_OnItemDropped(object sender, UI_RefSlot.OnItemDroppedEventArgs e)
    {
        //  Debug.Log(e.item);
        refiningSystem.TryAddItem(e.item);
    }
    private void UpdateUI()
    {
        //  Clear old items
        foreach(Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        //  Ask Refining System if it is empty
        //  If not spawn items
        if (!refiningSystem.IsEmpty())
        {
            Item item = refiningSystem.GetItem();
            CreateItemInput(item);
        }

        if (refiningSystem.GetOutputItem() != null)
        {
            CreateItemOutput(refiningSystem.GetOutputItem());
        }
    }

    private void CreateItemInput(Item item)
    {
        Transform uiItemTransform = Instantiate(pfIU_Item, itemContainer);
        RectTransform itemRectTransform = uiItemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = inputSlotTransform.GetComponent<RectTransform>().anchoredPosition;
        uiItemTransform.GetComponent<UI_Item>().SetItem(item);


        UI_Item uiItem = uiItemTransform.GetComponent<UI_Item>();
        //  Fires when object is dropped ontop of another
        uiItem.SetOnDropAction(() =>
        {
            Item draggedItem = UI_ItemDrag.Instance.GetItem();
            //  If both the items are the same merge
            if (item.ToString() == draggedItem.ToString())
            {
                refiningSystem.AddItemMergeAmount(item, draggedItem);
                draggedItem.RemoveFromItemHolder();
            }
        });
    }

    private void CreateItemOutput(Item item)
    { 
        Transform itemTransform = Instantiate(pfIU_Item, itemContainer);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = outputSlotTransform.GetComponent<RectTransform>().anchoredPosition;
        itemTransform.GetComponent<UI_Item>().SetItem(item);
    }

    public CanvasGroup GetCanvasGroup()
    {
        return canvasGroup;
    }
}
