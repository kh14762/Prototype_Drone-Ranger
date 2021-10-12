using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SojournerController : MonoBehaviour
{
    public CharacterController sojournerController;
    public Transform sojournerCamera;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //private float gravityModifier = 9.81f;
    public float gravityModifier = 9.81f;
    public float jumpForce = 3.0f;
    private float directionY;
    public float sojournerWalkSpeed = 15.0f;
    public float sojournerSprintSpeed = 25.0f;
    private float sojournerSpeed;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        // sprint speed
        if (sojournerController.isGrounded && Input.GetKey(KeyCode.LeftShift)){
            sojournerSpeed = sojournerSprintSpeed;
        }
        else
        {
            sojournerSpeed = sojournerWalkSpeed;
        }

        // movement
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized; // normalized prevents player from moving faster diagonally

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + sojournerCamera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // smooth turn angle
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // rotation to direction, to move player into direction of camera
            sojournerController.Move(moveDirection.normalized * sojournerSpeed * Time.deltaTime);
        }

        // jump
        if (Input.GetKey(KeyCode.Space) && sojournerController.isGrounded)
        {

            directionY = jumpForce;
        }

        directionY -= gravityModifier * Time.deltaTime;
        direction.y = directionY;
        sojournerController.Move(direction * Time.deltaTime);

    }

    

    // lock cursor to middle of screen if game is focused
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    //private Rigidbody sojournerRigidBody;
    //public float jumpForce;
    //public float gravityModifier;
    //public bool isOnGround = true;
    //public float speed = 20.0f;
    //private float forwardInput;
    //private float horizontalInput;
    //private bool isUiVisible = true;
    //private bool isReceptUIVis = false;
    //private Receptacle receptacle;

    //private Inventory inventory;
    //[SerializeField] private UI_Inventory uiInventory;
    //private void Awake()
    //{

    //}
    //// Start is called before the first frame update
    //void Start()
    //{
    //    inventory = new Inventory(UseItem, 8);
    //    uiInventory.SetSojournerController(this);
    //    uiInventory.SetInventory(inventory);

    //    //  Test adding pickup objects to scene
    //    ItemWorld.SpawnItemWorld(new Vector3(0, 1, 3), new Item { itemType = Item.ItemType.Cube, amount = 1 });
    //    ItemWorld.SpawnItemWorld(new Vector3(3, 1, 0), new Item { itemType = Item.ItemType.Cube, amount = 1 });
    //    ItemWorld.SpawnItemWorld(new Vector3(0, 1, -3), new Item { itemType = Item.ItemType.Cube, amount = 1 });

    //    sojournerRigidBody = GetComponent<Rigidbody>();
    //    Physics.gravity *= gravityModifier;

    //    receptacle = GameObject.Find("Receptacle").GetComponent<Receptacle>();
    //    Debug.Log(receptacle);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    /*-------------------------------Player input starts here-------------------------*/

    //    if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
    //    {
    //        sojournerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //        isOnGround = false;
    //    }

    //    forwardInput = Input.GetAxis("Vertical");
    //    horizontalInput = Input.GetAxis("Horizontal");

    //    // Move Forward or backwards when w or s key is pressed
    //    transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
    //    transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

    //    //  Toggle Player Inventory
    //    if (Input.GetKeyDown(KeyCode.Tab) && isUiVisible == true)
    //    {
    //        HideUI();
    //        SetIsUiVisible(false);
    //    } else if (Input.GetKeyDown(KeyCode.Tab) && isUiVisible == false)
    //    {
    //        ShowUI();
    //        SetIsUiVisible(true);
    //    }

    //    // Toggle Receptacle UI with E
    //    if (Input.GetKeyDown(KeyCode.E) && receptacle.GetIsPlayerColliding() == true)
    //    {
    //        if (isReceptUIVis == false)
    //        {
    //            receptacle.Interact();
    //            isReceptUIVis = true;
    //            if (GetIsUiVisible() == false)
    //            {
    //                ShowUI();
    //                SetIsUiVisible(true);
    //            }
    //        } else
    //        {
    //            receptacle.HideUI();
    //            isReceptUIVis = false;
    //            HideUI();
    //            SetIsUiVisible(false);
    //        }

    //    }


    //    /*-------------------------------Player input ends here---------------------------*/
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    isOnGround = true;
    //}

    //private void OnTriggerEnter(Collider other)
    //{

        
    //    ItemWorld itemWorld = other.GetComponent<ItemWorld>();
    //    if (itemWorld != null)
    //    {
    //        inventory.AddItemMergeAmount(itemWorld.GetItem());
    //        itemWorld.DestroySelf();
    //    }
    //}

    //public Vector3 GetPosition()
    //{
    //    return transform.position;
    //}

    //public void UseItem(Item inventoryItem)
    //{
    //    Debug.Log("Use Item: " + inventoryItem);
    //}

    //public void SetIsUiVisible(bool isUiVisible)
    //{
    //    this.isUiVisible = isUiVisible;
    //}
    //public bool GetIsUiVisible()
    //{
    //    return isUiVisible;
    //}

    //public void ShowUI()
    //{
    //    uiInventory.GetCanvasGroup().alpha = 1f;
    //    uiInventory.GetCanvasGroup().blocksRaycasts = true;
    //}
    //public void HideUI()
    //{
    //    uiInventory.GetCanvasGroup().alpha = 0f;
    //    uiInventory.GetCanvasGroup().blocksRaycasts = false;
    //}
}
