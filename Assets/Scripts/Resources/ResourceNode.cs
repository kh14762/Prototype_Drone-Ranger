using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResourceNode : MonoBehaviour
{
    private GameObject player;
    private Inventory playerInventory;
    private bool isCoroutineRunning;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sojourner");
        playerInventory = player.GetComponent<Inventory>();
        isCoroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= 5)
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (isCoroutineRunning)
                {
                    Debug.Log("Starting Coroutine");
                    StartCoroutine(Countdown());
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    // Raw material corountine 
    IEnumerator Countdown()
    {
        //Add materials to player inventory here
        playerInventory.AddItem(new Item { itemType = Item.ItemType.MetalScrap, amount = 1 });
        yield return new WaitForSeconds(3);
    }
}
