using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SojournerController : MonoBehaviour
{
    public Transform sojournerCamera;
    private Rigidbody sojournerRigidBody;
    private GameManager gameManager;
    public PlayerStats playerStats;
    public CinemachineFreeLook cinemachineFreeLook;
    private int camXAxisSpeed = 400, camYAxisSpeed = 2; // allow user to change

    public float jumpForce;
    public float gravityModifier;
    public float verticalInput;
    public float horizontalInput;
    public bool isOnGround = true;
    private bool isUiVisible = true;


    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    public float sojournerWalkSpeed = 15.0f;
    public float sojournerSprintSpeed = 25.0f;
    public float sojournerSpeed;

    public Vector3 MoveDirection;

    public bool enableInput;

    private UI_Controller uiController;
    //  Manufactoring Systems
    private Receptacle receptacle;
    private RefiningSystem refSystem;
    private Printer printer;
    //  Manufactoring Systems UI
    private bool isReceptUIVis = false;
    private bool isRefUIVis = false;
    private UI_Printer ui_Printer;
    //public List<ManufactoringSystem> manuSystemList;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        sojournerCamera = GameObject.Find("Main Camera").transform;
        cinemachineFreeLook = GameObject.Find("Third Person Camera").GetComponent<CinemachineFreeLook>();

        uiController = gameManager.GetComponent<UI_Controller>();
        inventory = new Inventory(UseItem, 8);
        uiInventory.SetSojournerController(this);
        uiInventory.SetInventory(inventory);

        //  Test adding pickup objects to scene
        /*for (int i = 0; i < 12; i += 3)
        {
            for (int j = 0; j < 12; j += 3) {
                ItemWorld.SpawnItemWorld(new Vector3(i, 1.25f, j), new Item { itemType = Item.ItemType.MetalScrap, amount = 1 });
            }
        }*/
        //ItemWorld.SpawnItemWorld(new Vector3(0, 1, 3), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(3, 1, 0), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(0, 1, -3), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        sojournerRigidBody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        //refSystem = GameObject.Find("RefiningStation").GetComponent<RefiningSystem>();
        //printer = GameObject.Find("3DPrinter").GetComponent<Printer>();

        HideUI();
        SetIsUiVisible(false);
        enableInput = true;

        // Lock cursor when playing
        //Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
        //Cursor.lockState = CursorLockMode.Locked; // keeps curose locked at the center of screen

        


        //  Set ui_Printer
        //ui_Printer = GameObject.Find("UI_Printer").GetComponent<UI_Printer>();
    }


    void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            if (enableInput)
            {
                // movement
                verticalInput = Input.GetAxis("Vertical");
                horizontalInput = Input.GetAxis("Horizontal");

                // camera rotation
                Vector3 forward = sojournerCamera.transform.forward;
                Vector3 right = sojournerCamera.transform.right;
                right.y = 0;
                forward.y = 0;
                forward.Normalize();
                right.Normalize();

                // Move Direction
                MoveDirection = forward * verticalInput + right * horizontalInput;

                // Move sojourner
                sojournerRigidBody.velocity = new Vector3(MoveDirection.x * sojournerSpeed, sojournerRigidBody.velocity.y, MoveDirection.z * sojournerSpeed);

                // Rotate sojourner in the direction they are moving
                if (MoveDirection != new Vector3(0, 0, 0))
                {
                    transform.rotation = Quaternion.LookRotation(MoveDirection);
                }
                // else
                //{
                // back always faces the camera, when moving AND when standing still.

                //transform.rotation = Quaternion.LookRotation(forward);
                //}

            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            //----------------------------------------------------------Player Input----------------------------------------------------//
            // sprint speed
            if (Input.GetKey(KeyCode.LeftShift) && isOnGround && playerStats.currentStamina > 0)
            {
                sojournerSpeed = sojournerSprintSpeed;

            }
            else
            {
                sojournerSpeed = sojournerWalkSpeed;
            }

            // jump
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                sojournerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }

            //----------------------------------------------------------Player Inventory----------------------------------------------------//
            //  Toggle Player Inventory
            if (Input.GetKeyDown(KeyCode.Tab) && isUiVisible == true)
            {
               // Cursor.lockState = CursorLockMode.Locked; // lock cursor
                // enable camera rotation
                cinemachineFreeLook.m_XAxis.m_MaxSpeed = camXAxisSpeed;
                cinemachineFreeLook.m_YAxis.m_MaxSpeed = camYAxisSpeed;
                HideUI();
                SetIsUiVisible(false);
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && isUiVisible == false)
            {
               // Cursor.lockState = CursorLockMode.None; // unlock cursor
                // disable camera rotation
                cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0;
                cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0;
                ShowUI();
                SetIsUiVisible(true);
            }

            //  TODO:
            //  using a list to store a manufactoring systems will cut back on dupication code
            //for (int i = 0; i < manuSystemList.Count; i++)
            //{
            //    if (interact && manuSystemList[i].GetIsPlayerColliding() == true)
            //    {
            //        if (!manuSystemList[i].GetIsUIVisible())
            //        {
            //            Cursor.lockState = CursorLockMode.None; // unlock cursor
            //            manuSystemList[i].Interact();
            //            manuSystemList[i].SetIsUIVisible(true);
            //            if (GetIsUiVisible() == false)
            //            {
            //                ShowUI();
            //                SetIsUiVisible(true);
            //            }
            //        } else
            //        {
            //            Cursor.lockState = CursorLockMode.Locked; // lock cursor
            //            manuSystemList[i].HideUI();
            //            isRefUIVis = false;
            //            HideUI();
            //            SetIsUiVisible(false);
            //        }
            //    }
            //}
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItemMergeAmount(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void UseItem(Item inventoryItem)
    {
        Debug.Log("Use Item: " + inventoryItem);
    }

    public void SetIsUiVisible(bool isUiVisible)
    {
        this.isUiVisible = isUiVisible;
    }
    public bool GetIsUiVisible()
    {
        return isUiVisible;
    }

    public void SetIsRefUIVis(bool isRefUIVis)
    {
        this.isRefUIVis = isRefUIVis;
    }
    public bool GetIsRefUIVis()
    {
        return isRefUIVis;
    }
    public void ShowUI()
    {
        uiInventory.GetCanvasGroup().alpha = 1f;
        uiInventory.GetCanvasGroup().blocksRaycasts = true;
    }
    public void HideUI()
    {
        uiInventory.GetCanvasGroup().alpha = 0f;
        uiInventory.GetCanvasGroup().blocksRaycasts = false;
    }
}



