using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMesh : MonoBehaviour
{
    public Transform pfItemWorld;
   public static ItemMesh Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Mesh cubeMesh;


}
