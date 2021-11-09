using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawnController : MonoBehaviour
{
    private Bounds bounds;
    public GameObject resourcePrefab;
    public Terrain IslandTerrain;
    public LayerMask TerrainLayer;


    // need spawn rates and max spawn amount (from kev)
    private float spawnRate = 2f;
    private int spawnMax = 20;
    private Vector3 spawnPos;
    private int resourceCount;

    // Start is called before the first frame update
    void Start()
    {
        // going to use bounds of a "spawn area" collider for the x and z coordinates
        // and get y from terrain height
        bounds = GetComponent<Collider>().bounds;
        Debug.Log("Bounds min: " + bounds.min + " Bounds max: " + bounds.max);
    }
        
    // Update is called once per frame
    void Update()
    {
        resourceCount = GameObject.FindGameObjectsWithTag("Resource1").Length;
        
        if (resourceCount < spawnMax)
        {
            //SpawnResource();
        }
    }

    /*
    IEnumerator SpawnResource()
    {
        spawnPos = new Vector3(Random.Range(bounds.min.x, bounds.max.x),
            ,
            Random.Range(bounds.min.z, bounds.max.z));
        Instantiate(resourcePrefab, , Quaternion.identity);
        yield return new WaitForSeconds(spawnRate);
    }*/
}
