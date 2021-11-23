using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class Receptacle_UI : MonoBehaviour
{
    [SerializeField] private Transform pfUI_Item;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private Inventory inventory;
    private Receptacle receptacle;
    private CanvasGroup canvasGroup;




    // Start is called before the first frame update
    private void Start()
    {

        itemSlotContainer = transform.Find("r_itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("r_itemSlotTemplate");

        itemSlotTemplate.gameObject.SetActive(false);
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public void SetReceptacle(Receptacle receptacle)
    {
        this.receptacle = receptacle;
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    public void RefreshInventoryItems()
    {

        foreach (Transform child in itemSlotContainer)
        {
            Debug.Log(child);
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 75f;
        foreach (Inventory.InventorySlot inventorySlot in inventory.GetInventorySlotArray())
        {
            Item item = inventorySlot.GetItem();
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                Debug.Log("Left Clicked on UI_Inventory");
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Debug.Log("Right Clicked on UI_Inventory");
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);


            if (!inventorySlot.IsEmpty())
            {
                // Not Empty, has Item
                Transform uiItemTransform = Instantiate(pfUI_Item, itemSlotContainer);
                uiItemTransform.GetComponent<RectTransform>().anchoredPosition = itemSlotRectTransform.anchoredPosition;
                UI_Item uiItem = uiItemTransform.GetComponent<UI_Item>();
                //Get uiText
                uiItem.SetAmountText(5);
                uiItem.SetItem(item);


                //  Drag on same item to merge together
                //  below code is jank
                uiItem.SetOnDropAction(() =>
                {
                    Debug.Log("Dropped on ui_Item!");
                        //  Check if item is stackable
                    Item draggedItem = UI_ItemDrag.Instance.GetItem();
                    Inventory.InventorySlot tmpInventorySlot = inventorySlot;
                    Debug.Log(draggedItem.amount);
                    if (tmpInventorySlot.GetItem().ToString() == draggedItem.ToString())
                    {
                        draggedItem.RemoveFromItemHolder();
                        inventory.AddItemMergeAmount(item, draggedItem, tmpInventorySlot);
                    }
                });
            }

            UI_ItemSlot uiItemSlot = itemSlotRectTransform.GetComponent<UI_ItemSlot>();
            Inventory.InventorySlot tmpInventorySlot = inventorySlot;
            uiItemSlot.SetOnDropAction(() =>
            {
                if (tmpInventorySlot.IsEmpty())
                {
                        // Dropped on this UI Item Slot
                        Item draggedItem = UI_ItemDrag.Instance.GetItem();
                    draggedItem.RemoveFromItemHolder();
                    inventory.AddItem(draggedItem, tmpInventorySlot);
                }
            });
            x++;
            if (x >= 8)
            {
                x = 0;
                y++;
            }
        }
    }

    public CanvasGroup GetCanvasGroup()
    {
        return canvasGroup;
    }

    public void SetCanvasGroup(CanvasGroup canvasGroup)
    {
        this.canvasGroup = canvasGroup;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
