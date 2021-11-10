using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform sojourner;
    Vector3 offset = new Vector3(0, 20, 0);

    // Start is called before the first frame update
    void Start()
    {
        sojourner = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // follow sojourner's position
        Vector3 position = sojourner.position + offset;
        transform.position = position;

        // rotate minimapww
        //transform.rotation = Quaternion.Euler(90f, sojourner.eulerAngles.y, 0f);
    }
}
