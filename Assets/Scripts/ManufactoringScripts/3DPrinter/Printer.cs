using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    private Item item;
    private List<Item> partList;
    private Item outputItem;
    private bool isPlayerColliding;
    public event EventHandler OnChange;

    [SerializeField] private UI_Printer uiPrinter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsEmpty()
    {
        return item == null; // return false true if null
    }

    public Item GetItem()
    {
        return item;
    }

    public Item GetOutputItem()
    {
        return outputItem;
    }
    public void IncreaseItemAmount()
    {
        GetItem().amount++;
        OnChange?.Invoke(this, EventArgs.Empty);
    }

    //public void RemoveItem()
    //{
    //    SetItem(null);
    //}

    //public void DecreaseItemAmount()
    //{
    //    if (GetItem() != null)
    //    {
    //        GetItem().amount--;
    //        if (GetItem().amount == 0)
    //        {
    //            RemoveItem();
    //        }
    //        OnChange?.Invoke(this, EventArgs.Empty);
    //    }
    //}



    //-------------------------------Player Interaction----------------------------------//
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetIsPlayerColliding(true);
            Debug.Log("Player is colliding with 3D Printer");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetIsPlayerColliding(false);
            Debug.Log("player is no longer colliding with 3D Printer");
        }
    }

    public void SetIsPlayerColliding(bool isPlayerColliding)
    {
        this.isPlayerColliding = isPlayerColliding;
    }
    //---------------------------End of Player Interaction------------------------------//
}
