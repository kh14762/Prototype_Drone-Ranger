using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundaries : MonoBehaviour
{
    private Rigidbody rb;
    private SojournerController sj;

    //hard coded for simplicity
    public float playerMinY = -12.5f;
    private float playerPosX;
    private float playerPosZ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sj = GetComponent<SojournerController>(); //for isOnGround check in future

        playerPosX = transform.position.x;
        playerPosZ = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        //-------THIS WILL BREAK IF YOU JUMP ONTO WATER-------
        // Need to find a way to wait until scene is fully loaded and then do update
        // so we can use isOnGround
        //if (transform.position.y > playerMinY)
        //{
        //    playerPosX = transform.position.x;
        //    playerPosZ = transform.position.z;
        //}
        //else //if pos < min || pos < min && !isOnGound mess around w this later
        //{
        //    transform.position = new Vector3(playerPosX, playerMinY, playerPosZ);
        //}

    }

    //First attempt: this isn 't consistent for some f%!#ing reason
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            Debug.Log("water reached");
        }
    }

}



//waterPosY = GameObject.FindGameObjectWithTag("Water").GetComponent<BoxCollider>().bounds.max.y;
//this gives a different value from the min value I need to make sure they can't go on top of the water
