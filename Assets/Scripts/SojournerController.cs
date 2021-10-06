using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SojournerController : MonoBehaviour
{
    public CharacterController sojournerController;
    public Transform sojournerCamera;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private float gravityModifier = 9.81f;
    public float jumpForce = 3.0f;
    private float directionY;
    public float sojournerWalkSpeed = 5.0f;
    public float sojournerSprintSpeed = 10.0f;
    public float sojournerSpeed;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        // sprint speed
        if (sojournerController.isGrounded && Input.GetKey(KeyCode.LeftShift)){
            sojournerSpeed = sojournerSprintSpeed;
        }
        else
        {
            sojournerSpeed = sojournerWalkSpeed;
        }

        // movement
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized; // normalized prevents player from moving faster diagonally

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + sojournerCamera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // smooth turn angle
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // rotation to direction, to move player into direction of camera
            sojournerController.Move(moveDirection.normalized * sojournerSpeed * Time.deltaTime);
        }

        // jump
        if (Input.GetKey(KeyCode.Space) && sojournerController.isGrounded)
        {

            directionY = jumpForce;
        }

        directionY -= gravityModifier * Time.deltaTime;
        direction.y = directionY;
        sojournerController.Move(direction * Time.deltaTime);

    }

    

    // lock cursor to middle of screen if game is focused
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
