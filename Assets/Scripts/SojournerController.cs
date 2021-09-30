using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SojournerController : MonoBehaviour
{
    private Rigidbody sojournerRigidBody;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        Debug.Log(new Item {itemType = Item.ItemType.Cube, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 1, 3), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(3, 1, 0), new Item { itemType = Item.ItemType.Cube, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 1, -3), new Item { itemType = Item.ItemType.Cube, amount = 1 });

        sojournerRigidBody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            sojournerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
