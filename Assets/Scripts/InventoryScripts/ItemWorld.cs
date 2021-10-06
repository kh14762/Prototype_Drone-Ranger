using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private MeshFilter meshFilter;
    private Sprite sprite;
    public Mesh mesh;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();   
    }
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemMesh.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + UtilsClass.GetRandomDir() * 5f, item);
        itemWorld.GetComponent<Rigidbody>().AddForce(randomDir * 5f, ForceMode.Impulse);
        return itemWorld;
    }
   public void SetItem(Item item)
    {
        this.item = item;
        meshFilter.mesh = item.GetMesh();
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
