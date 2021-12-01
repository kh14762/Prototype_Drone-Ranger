using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResourceNode : MonoBehaviour
{
    private GameObject player;
    public Item[] rawMaterials;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sojourner");

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && Input.GetKeyDown(KeyCode.F))
        {
            while (true)
            {
                StartCoroutine(Countdown());
            }
        }
    }

    // Raw material corountine 
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(3);

    }
}
