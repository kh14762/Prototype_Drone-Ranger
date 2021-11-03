using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefiningSystem : MonoBehaviour, IItemHolder, IInteractable
{
    private Item item;
    private Dictionary<Item.ItemType, Item.ItemType> recipeDictionary;
    private Item outputItem;
    public event EventHandler OnChange;

    Coroutine refiningCoroutine;
    public ProgressBar progressBar;
    private int refiningTime;
    private float progressTime;
    private bool isRefining;

    //  player collision
    private bool isPlayerColliding;
    private GameObject sojourner;
    private SojournerController s_controller;
    [SerializeField] private UI_RefiningSystem uiRefiningSystem;


    void Start()
    {
        uiRefiningSystem.SetRefiningSystem(this);

        //-------------------------------------------------------------Recipes-------------------------------------------------------------//

        recipeDictionary = new Dictionary<Item.ItemType, Item.ItemType>();
        //  RefinedMetal
        Item.ItemType refinedMetalRecipe;
        refinedMetalRecipe = Item.ItemType.MetalScrap;
        recipeDictionary[Item.ItemType.RefinedMetal] = refinedMetalRecipe;

        //  RefinedPolymer
        Item.ItemType refinedPolymerRecipe;
        refinedPolymerRecipe = Item.ItemType.PolymerScrap;
        recipeDictionary[Item.ItemType.RefinedPolymer] = refinedPolymerRecipe;

        //  RefinedSilicon
        Item.ItemType refinedSiliconRecipe;
        refinedSiliconRecipe = Item.ItemType.SiliconScrap;
        recipeDictionary[Item.ItemType.RefinedSilicon] = refinedSiliconRecipe;

        //---------------------------------------------------End of Recipes---------------------------------------------------------------//

        //  Progress Bar system
        isRefining = false;
        refiningTime = 1;
        progressTime = 0;

        //  Set Sojourner controller
        sojourner = GameObject.Find("Sojourner");
        s_controller = sojourner.GetComponent<SojournerController>();
        this.HideUI();
    }

    void Update()
    {
        if (isRefining == true)
        {
            //  start countdown
            progressTime += Time.deltaTime;
            progressBar.SetTime(progressTime / (float)refiningTime);
        }
        else
        {
            progressTime = 0;
            progressBar.SetTime(progressTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetIsPlayerColliding(true);
            Debug.Log("player colliding with Refining Station");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetIsPlayerColliding(false);
            if (s_controller.GetIsRefUIVis() == true)
            {
                s_controller.HideUI();
                s_controller.SetIsRefUIVis(false);
            }
            s_controller.SetIsRefUIVis(false);
            s_controller.SetIsUiVisible(false);
            Debug.Log("player no longer with Refining Station");
            this.HideUI();
        }
    }

    public bool IsEmpty()
    {
        // return true if null else return false
        return item == null;
    }

    public Item GetItem()
    {
        return item;
    }
    public void IncreaseItemAmount()
    {
        GetItem().amount++;
        OnChange?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseItemAmount()
    {
        //Debug.Log("hello from DecreaseItemAmount()");
        if (GetItem() != null)
        {
            GetItem().amount--;
            if (GetItem().amount == 0)
            {
                RemoveItem();
            }
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //??
            //Debug.Log("Im null");
        }
    }

    public void DecreaseOutputItemAmount()
    {
        if (GetOutputItem() != null)
        {
            GetOutputItem().amount--;
            if (GetOutputItem().amount == 0)
            {
                RemoveItem();
            }
            OnChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public void RemoveItem()
    {
        SetItem(null);
    }

    //  This is called when Item is dropped on UI RefiningSystem
    public bool TryAddItem(Item item)
    {
        if (IsEmpty())
        {
            SetItem(item);
            return true;
        }
        else
        {
            if (item.itemType == GetItem().itemType)
            {
                //IncreaseItemAmount();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //  Triggered when ever the user removes an item from the output slot
    public void RemoveItem(Item item)
    {
        if (item == outputItem)
        {
            //  Remove output Item
            SetOutPutItem(null);
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //  Remove Item from Input Slot
            if (refiningCoroutine != null)
            {
                StopCoroutine(refiningCoroutine);
            }
            RemoveItem();
            isRefining = false;//stops the progress bar and sets it to zero
            OnChange?.Invoke(this, EventArgs.Empty);

        }
    }

    public void SetItem(Item item)
    {
        if (item != null)
        {
            item.RemoveFromItemHolder();
            item.SetItemHolder(this);
        }
        this.item = item;
        RefineAllInputMaterials();

        OnChange?.Invoke(this, EventArgs.Empty);
    }

    //  Trigger in SetItem()
    //  Should immediately start when an item is placed in the holder
    public void RefineAllInputMaterials()
    {
        //  Store coroutine in a variable, can use this var to stop the coroutine
        refiningCoroutine = StartCoroutine(RefiningCoroutine());
    }

    IEnumerator RefiningCoroutine()
    {
        //  get the predicted item type
        Item.ItemType predictedOutput = GetRecipeOutput();
        Debug.Log(predictedOutput);
        while (item != null)
        {
            if (outputItem != null)
            {
                //  if predicted output is not equal to the output item type then break from loop
                if (predictedOutput != outputItem.itemType)
                {
                    break;
                }
            }
            isRefining = true;
            yield return new WaitForSeconds(refiningTime);
            isRefining = false;
            progressTime = 0;

            if (outputItem == null)
            {
                CreateOutput();
            }
            else
            {
                //  Add to output item
                outputItem.amount++;
            }
            DecreaseItemAmount(); //Input Item
            //  if the player is in the middle of dragging an item, Hides it to prevent bug  
            UI_ItemDrag.Instance.Hide();
            OnChange.Invoke(this, EventArgs.Empty);
        }
    }

    public void AddItem(Item item)
    {
        throw new NotImplementedException();
    }

    public bool CanAddItem()
    {
        throw new NotImplementedException();
    }

    private Item.ItemType GetRecipeOutput()
    {
        foreach (Item.ItemType recipeItemType in recipeDictionary.Keys)
        {
            Item.ItemType recipe = recipeDictionary[recipeItemType];

            bool completeRecipe = true;
            if (recipe != Item.ItemType.None)
            {
                if (IsEmpty() || GetItem().itemType != recipe)
                {
                    //  empty input slot or different itemType placed in input slot
                    completeRecipe = false;
                }
            }
            if (completeRecipe)
            {
                return recipeItemType;
            }
        }
        return Item.ItemType.None;
    }

    //  This function gets the corresponding item output for the input item
    //  Creates output item and sets it in the holder
    private void CreateOutput()
    {
        Item.ItemType recipeOutput = GetRecipeOutput();
        if (recipeOutput == Item.ItemType.None)
        {
            outputItem = null;
        }
        else
        {
            outputItem = new Item { itemType = recipeOutput, amount = 1 };
            outputItem.SetItemHolder(this);

        }
    }

    public Item GetOutputItem()
    {
        return outputItem;
    }

    public void SetOutPutItem(Item outputItem)
    {
        this.outputItem = outputItem;
    }

    public void AddItemMergeAmount(Item slotItem, Item draggedItem)
    {
        if (draggedItem.IsStackable())
        {
            slotItem.amount += draggedItem.amount;
            //draggedItem.SetItemHolder(this);
        }
        OnChange.Invoke(this, EventArgs.Empty);
    }

    public void ShowUI()
    {
        uiRefiningSystem.GetCanvasGroup().alpha = 1f;
        uiRefiningSystem.GetCanvasGroup().blocksRaycasts = true;
    }
    public void HideUI()
    {
        uiRefiningSystem.GetCanvasGroup().alpha = 0f;
        uiRefiningSystem.GetCanvasGroup().blocksRaycasts = false;
    }

    public void SetIsPlayerColliding(bool isPlayerColliding)
    {
        this.isPlayerColliding = isPlayerColliding;
    }

    public bool GetIsPlayerColliding()
    {
        return isPlayerColliding;
    }

    public void Interact()
    {
        ShowUI();
    }
}
