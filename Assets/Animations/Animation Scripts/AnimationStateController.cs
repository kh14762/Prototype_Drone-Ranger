using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    SojournerController sjController;
    // Start is called before the first frame update
    void Start()
    {
        sjController = GameObject.Find("Sojourner").GetComponent<SojournerController>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isJumping = animator.GetBool(isJumpingHash);

        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");

        bool jumpPressed = Input.GetKey("space");
        bool runPressed = Input.GetKey("left shift");

        if (forwardPressed || leftPressed || rightPressed || backPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (!(forwardPressed || leftPressed || rightPressed || backPressed) && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && ((forwardPressed || leftPressed || rightPressed || backPressed) && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!(forwardPressed || leftPressed || rightPressed || backPressed) || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

        if (!sjController.isOnGround)
        {
            animator.SetBool(isJumpingHash, true);
        }

        if (sjController.isOnGround)
        {
            animator.SetBool(isJumpingHash, false);
        }
    }
}
