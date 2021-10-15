using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SojournerController : MonoBehaviour
{
    public Transform sojournerCamera;
    private Rigidbody sojournerRigidBody;
    public float jumpForce;
    public float gravityModifier;
    public float verticalInput;
    public float horizontalInput;
    public bool isOnGround = true;
    private bool isUiVisible = true;
    private bool isReceptUIVis = false;
    private Receptacle receptacle;

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    public float sojournerWalkSpeed = 15.0f;
    public float sojournerSprintSpeed = 25.0f;
    private float sojournerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory(UseItem, 8);
        uiInventory.SetSojournerController(this);
        uiInventory.SetInventory(inventory);

        //  Test adding pickup objects to scene
        for (int i = 0; i < 12; i += 3)
        {
            for (int j = 0; j < 12; j += 3) {
                ItemWorld.SpawnItemWorld(new Vector3(i + 10, 1.25f, j), new Item { itemType = Item.ItemType.MetalScrap, amount = 1 });
            }
        }
        //ItemWorld.SpawnItemWorld(new Vector3(0, 1, 3), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(3, 1, 0), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(0, 1, -3), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        sojournerRigidBody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        receptacle = GameObject.Find("Receptacle").GetComponent<Receptacle>();
        Debug.Log(receptacle);
        HideUI();
        SetIsUiVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Lock cursor when playing
        //Cursor.lockState = CursorLockMode.Locked;
        //  TODO: unlock when using inventory

        //----------------------------------------------------------Player Input----------------------------------------------------//
        // sprint speed
        if (Input.GetKey(KeyCode.LeftShift) && isOnGround)
        {
            sojournerSpeed = sojournerSprintSpeed;
        }
        else
        {
            sojournerSpeed = sojournerWalkSpeed;
        }

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
        Vector3 MoveDirection = forward * verticalInput + right * horizontalInput;

        // Move sojourner
        sojournerRigidBody.velocity = new Vector3(MoveDirection.x * sojournerSpeed, sojournerRigidBody.velocity.y, MoveDirection.z * sojournerSpeed);

        // Rotate sojourner in the direction they are moving
        if (MoveDirection != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.LookRotation(MoveDirection);
        }
        

        /*// Move Forward or backwards when w or s key is pressed
        transform.Translate(Vector3.forward * Time.deltaTime * sojournerSpeed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * sojournerSpeed * horizontalInput);*/

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
            HideUI();
            SetIsUiVisible(false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isUiVisible == false)
        {
            ShowUI();
            SetIsUiVisible(true);
        }

        // Toggle Receptacle UI with E
        if (Input.GetKeyDown(KeyCode.E) && receptacle.GetIsPlayerColliding() == true)
        {
            if (isReceptUIVis == false)
            {
                receptacle.Interact();
                isReceptUIVis = true;
                if (GetIsUiVisible() == false)
                {
                    ShowUI();
                    SetIsUiVisible(true);
                }
            }
            else
            {
                receptacle.HideUI();
                isReceptUIVis = false;
                HideUI();
                SetIsUiVisible(false);
            }
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
    public void SetIsReceptUIVis(bool isReceptUIVis)
    {
        this.isReceptUIVis = isReceptUIVis;
    }
    public bool GetIsReceptUIVis()
    {
        return isReceptUIVis;
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
