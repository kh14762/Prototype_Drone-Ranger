using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{

    public GameObject nodePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Instantiate(nodePrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void 
}
