using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed = 2.0f;
    // Accuracy
    public float accuracy = 0.01f;
    // Object target
    private Transform goal;
    private Transform goal2;
    public float inventorySpace = 5.0f;

    public GameObject materials;
    

    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void LateUpdate() {
        if (inventorySpace != 0)
        {
          MoveToMats();  
        }else
        {
            MoveToReceptacle();
        }
        
    }

    private void MoveToMats(){
        goal = GameObject.FindGameObjectWithTag("Mats").transform;
        this.transform.LookAt(goal.position);
        Vector3 direction = goal.position - this.transform.position;

        if (direction.magnitude > accuracy) {
            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
    private void MoveToReceptacle()
    {
        goal2 = GameObject.FindGameObjectWithTag("Receptacle").transform;
        this.transform.LookAt(goal2.position);
        Vector3 direction = goal2.position - this.transform.position;

        if (direction.magnitude > accuracy) {
            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Mats"))
        {
            Destroy(other.gameObject);
            inventorySpace--;
        } else if (other.CompareTag("Receptacle"))
        {
            inventorySpace = 5;
        }
    }
}
