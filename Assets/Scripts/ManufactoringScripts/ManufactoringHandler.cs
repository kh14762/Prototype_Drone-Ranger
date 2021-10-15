using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManufactoringHandler : MonoBehaviour
{
    private void Start()
    {
        RefiningStationSystem rs_System = new RefiningStationSystem();
        Item item = new Item { itemType = Item.ItemType.Cube, amount = 1 };
        rs_System.SetItem(item);
        Debug.Log(rs_System.GetItem());
    }
}
