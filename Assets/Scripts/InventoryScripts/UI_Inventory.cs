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

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        itemSlotTemplate.gameObject.SetActive(false);
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
            uiItemSlot.SetOnDropAction(() =>
            {
                // Dropped on this UI Item Slot
                Item draggedItem = UI_ItemDrag.Instance.GetItem();
                Inventory.InventorySlot tmpInventorySlot = inventorySlot;
                draggedItem.RemoveFromItemHolder();
                inventory.AddItem(draggedItem, tmpInventorySlot);
            });


            // Drop item
            //Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            //inventory.RemoveItem(item);
            //ItemWorld.DropItem(player.GetPosition(), duplicateItem);

            //Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            //image.sprite = item.GetSprite();

            //TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            //if (item.amount > 1)
            //{
            //    uiText.SetText(item.amount.ToString());
            //}
            //else
            //{
            //    uiText.SetText("");
            //}

            x++;
            if (x >= 4)
            {
                x = 0;
                y++;
            }
        }
    }


}
