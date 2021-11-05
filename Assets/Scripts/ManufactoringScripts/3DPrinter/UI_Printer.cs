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
    private bool isUiVisible;

    void Start()
    {
        //  Set UI visibility to false
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        HideUI();
        SetIsUIVisible(false);


    }

    private void CreateItemInput(Item item)
    {
        //Transform uiItemTransform = Instantiate(pfIU_Item, itemContainer);
        //RectTransform itemRectTransform = uiItemTransform.GetComponent<RectTransform>();
        //itemRectTransform.anchoredPosition = inputSlotTransform.GetComponent<RectTransform>().anchoredPosition;
        //uiItemTransform.GetComponent<UI_Item>().SetItem(item);

        //UI_Item uiItem = uiItemTransform.GetComponent<UI_Item>();

    }

    private void CreateItemOutput(Item item)
    {

    }

    //----------------------------------------------------------UI Visibility Control----------------------------------------------------//
    public void SetIsUIVisible(bool isUiVisible)
    {
        this.isUiVisible = isUiVisible;
    }
    public bool GetIsUIVisible()
    {
        return isUiVisible;
    }
    public void ShowUI()
    {
        GetCanvasGroup().alpha = 1f;
        GetCanvasGroup().blocksRaycasts = true;
    }
    public void HideUI()
    {
        GetCanvasGroup().alpha = 0f;
        GetCanvasGroup().blocksRaycasts = false;
    }
    public CanvasGroup GetCanvasGroup()
    {
        return canvasGroup;
    }
    //----------------------------------------------------End of UI Visibility Control----------------------------------------------------//

}
