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
    public GameObject materials;

    //sight range
    public LayerMask whatIsMat;
    public bool matInSightRange;
    public float sightRange;
    public UnityEngine.AI.NavMeshAgent agent;


    //inventory space and UI
    public float inventorySpace = 6.0f;
    private GameObject sojourner;
    // private SojournerController s_controller; --for drone interactions
    // private bool isPlayerColliding;
    private Inventory inventory;
    private UI_Inventory ui_inventory;
    private Receptacle_UI receptacle_ui;
    [SerializeField] private Drone_UI drone_ui;
    private int numOfMats;
    //movement flags for animation controller
    private bool isMoving;




    // Start is called before the first frame update
    void Start() 
    {
        inventory = new Inventory(5);
        receptacle_ui = GameObject.FindGameObjectWithTag("Receptacle").GetComponent<Receptacle_UI>();
        //drone_ui.SetDrone(this);
        //drone_ui.SetInventory(inventory);
        sojourner = GameObject.Find("Sojourner");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //s_controller = sojourner.GetComponent<SojournerController>();
        //this.HideUI();
    }

    // Update is called once per frame
    void Update()
    { //replaces LateUpdate or change back it back to LateUpdate()
        matInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsMat);
        if(matInSightRange){
            if (inventorySpace != 0){ MoveToMats(); }
            else{ MoveToReceptacle(); }
        }else{
            MoveToReceptacle();
        }

        if (transform.hasChanged)
        {
            isMoving = true;
            transform.hasChanged = false;
        } else
        {
            isMoving = false;
        }
    }
    private void OnDrawGizmosSelected() //shows sightrange
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

     public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Mats");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void MoveToMats(){
        goal = GameObject.FindGameObjectWithTag("Mats").transform;
        agent.SetDestination(FindClosestEnemy().transform.position);
        
    }
    private void MoveToReceptacle()
    {
        goal2 = GameObject.FindGameObjectWithTag("Receptacle").GetComponent<BoxCollider>().transform;
        // Go to the edge of receptacle collider
        
        agent.SetDestination(goal2.position);
        agent.stoppingDistance = 3.2f;
    }
    private void OnTriggerEnter(Collider other)
    {
         //  Set bool is Player colliding to true if collider is player -- UI interaction
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     SetIsPlayerColliding(true);
        //     Debug.Log("player colliding with receptacle");
        // }


        if(other.CompareTag("Mats"))
        {
            Destroy(other.gameObject);
            Debug.Log("Picked up mat");
            inventorySpace--;
            numOfMats++;
        } else if (other.CompareTag("Receptacle"))
        {
            //Item[] items;
            inventorySpace = 5;
            //  if the drone contacts receptacle
            Debug.Log("In contact with receptacle");
            Receptacle receptacle = other.GetComponent<Receptacle>();
            Inventory receptacleInventory = receptacle.GetInventory();
            receptacleInventory.AddItem(new Item { itemType = Item.ItemType.MetalScrap, amount = numOfMats });
        }
    }

    private void DropOffItems(Collider other)
    {
           //  get receptacle inventory
        
        
        
    }

    //  functions for animation control
    public bool IsMoving
    {
        get { return isMoving; }
    }
    //UI Functions -- if want interaction with drone (not done)
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         SetIsPlayerColliding(false);
    //         //  tell the s* controller that receptacle UI is hidden
    //         //  Hide the player ui and tell controller
    //         if (s_controller.GetIsReceptUIVis() == true)
    //         {
    //             s_controller.HideUI();
    //             s_controller.SetIsUiVisible(false);
    //         }
    //         s_controller.SetIsReceptUIVis(false);
    //         Debug.Log("player no longer colliding with receptacle");
    //         //  Hide receptacle ui
    //         this.HideUI();
    //     }
    // }

    // public void Interact()
    // {
    //     ShowUI();
    // }

    // public void ShowUI()
    // {
    //     drone_ui.GetCanvasGroup().alpha = 1f;
    //     drone_ui.GetCanvasGroup().blocksRaycasts = true;
    // }

    // public void HideUI()
    // {
    //     drone_ui.GetCanvasGroup().alpha = 0f;
    //     drone_ui.GetCanvasGroup().blocksRaycasts = false;
    // }

    // public void SetIsPlayerColliding(bool isPlayerColliding)
    // {
    //     this.isPlayerColliding = isPlayerColliding;
    // }

    // public bool GetIsPlayerColliding()
    // {
    //     return isPlayerColliding;
    // }
}
