using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    Drone drone;
    // Start is called before the first frame update
    void Start()
    {
        //  get drone script from parent gameobject
        drone = transform.parent.GetComponent<Drone>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);

        if (drone.IsMoving)
        {
            animator.SetBool(isWalkingHash, true);
        } else
        {
            animator.SetBool(isWalkingHash, false);
        }
        
    }
}
