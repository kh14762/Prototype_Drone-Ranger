using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResourceNode : MonoBehaviour
{
    private GameObject player;
    private SojournerController sjController;
    private Inventory playerInventory;
    private bool isCoroutineRunning;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sojourner");
        sjController = player.GetComponent<SojournerController>();
        playerInventory = sjController.GetInventory();
        isCoroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= 5)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("player holding f key down");
                if (isCoroutineRunning)
                {
                    Debug.Log("Starting Coroutine");
                    StartCoroutine(Countdown());
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                Debug.Log("player no longer holding f key down");
            }
        }
    }

    // Raw material corountine 
    IEnumerator Countdown()
    {
        //Add materials to player inventory here
        Debug.Log(playerInventory);
        playerInventory.AddItem(new Item { itemType = Item.ItemType.MetalScrap, amount = 1 });
        yield return new WaitForSeconds(3);
    }
}
