using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private MeshFilter meshFilter;
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
   public void SetItem(Item item)
    {
        this.item = item;
        meshFilter.mesh = item.GetMesh();
    }
}
