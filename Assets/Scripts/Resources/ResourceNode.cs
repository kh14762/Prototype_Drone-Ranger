using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceNode : MonoBehaviour
{
    private GameObject player;
    private SojournerController sjController;
    private Inventory playerInventory;
    private bool isCoroutineRunning;
    public Item[] rawMaterials;
    public Slider resource;
    private Coroutine countdown;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sojourner");
        sjController = player.GetComponent<SojournerController>();
        isCoroutineRunning = false;
        resource.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // fill bar
        StartCoroutine(Countdown());
        // gather resource if player is close to a node
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= 5)
        {
            if (Input.GetKey(KeyCode.F) && resource.value == 100)
            {
                // insert item into inventory
                //StartCoroutine(Countdown());
                // reset progress bar
                playerInventory = sjController.GetInventory();
                playerInventory.AddItemMergeAmount(new Item { itemType = Item.ItemType.MetalScrap, amount = 1 });
                resource.value = 0;
            }
        }
    }
    // Raw material corountine 
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);
        resource.value += 1;
    }
}
