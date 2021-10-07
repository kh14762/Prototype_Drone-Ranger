using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SojournerController : MonoBehaviour
{
    private Rigidbody sojournerRigidBody;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public float speed = 20.0f;
    private float forwardInput;
    private float horizontalInput;

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory(UseItem, 8);
        uiInventory.SetSojournerController(this);
        uiInventory.SetInventory(inventory);

        //  Test adding pickup objects to scene
        ItemWorld.SpawnItemWorld(new Vector3(0, 1, 3), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(3, 1, 0), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 1, -3), new Item { itemType = Item.ItemType.Cube, amount = 1 });

        sojournerRigidBody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        /*-------------------------------Player input starts here-------------------------*/

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            sojournerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // Move Forward or backwards when w or s key is pressed
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        /*-------------------------------Player input ends here---------------------------*/

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
            inventory.AddItem(itemWorld.GetItem());
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
}
