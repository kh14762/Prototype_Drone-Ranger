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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sojourner");
        sjController = player.GetComponent<SojournerController>();
        playerInventory = sjController.GetInventory();
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

                // reset progress bar
                resource.value = 0;
            }



        }
    }

    // Raw material corountine 
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.1f);
        resource.value += 1;

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && Input.GetKeyDown(KeyCode.F))
        {
            while (true)
            {
                StartCoroutine(Countdown());
            }
        }
    }*/


}
