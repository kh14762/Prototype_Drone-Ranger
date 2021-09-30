using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMesh : MonoBehaviour
{
    public Transform pfItemWorld;
    public Mesh cubeMesh;
   public static ItemMesh Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }



}
