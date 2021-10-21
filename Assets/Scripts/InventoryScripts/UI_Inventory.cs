using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private Transform pfUI_Item;

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private SojournerController player;
    private Receptacle receptacle;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        itemSlotTemplate.gameObject.SetActive(false);
        canvasGroup = gameObject.GetComponent<CanvasGroup>();      
    }

    public void SetSojournerController(SojournerController player)
    {
        this.player = player;
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

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
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

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                Debug.Log("Left Clicked on UI_Inventory");    
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                Debug.Log("Right Clicked on UI_Inventory");
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);

            
                // Not Empty, has Item
            if (!inventorySlot.IsEmpty())
            {
                Transform uiItemTransform = Instantiate(pfUI_Item, itemSlotContainer);
                uiItemTransform.GetComponent<RectTransform>().anchoredPosition = itemSlotRectTransform.anchoredPosition;
                UI_Item uiItem = uiItemTransform.GetComponent<UI_Item>();
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
                // Dropped on this UI Item Slot
                Item draggedItem = UI_ItemDrag.Instance.GetItem();
                draggedItem.RemoveFromItemHolder();
                inventory.AddItem(draggedItem, tmpInventorySlot);
            });

            x++;
            if (x >= 4)
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

}
