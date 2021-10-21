using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RefiningSystem : MonoBehaviour
{
    [SerializeField] private Transform pfIU_Item;
    private Transform inputSlot;
    private Transform outputSlotTransform;
    private Transform itemContainer;
    private RefiningSystem refiningSystem;


    void Start()
    {
        inputSlot = transform.Find("InputSlot");
        outputSlotTransform = transform.Find("OutputSlot");
        itemContainer = transform.Find("ItemContainer");

        // Subscribe to OnItemDropped Event for inputSlot
        inputSlot.GetComponent<UI_RefSlot>().OnItemDropped += UI_RefiningSystem_OnItemDropped;

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
        Debug.Log(e.item);
        refiningSystem.TryAddItem(e.item);
    }
    private void UpdateUI()
    {
        foreach(Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        //  Ask Refining System if it is empty
        //  If not create item
        if (!refiningSystem.IsEmpty())
        {
            CreateItemInput(refiningSystem.GetItem());
        }
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
