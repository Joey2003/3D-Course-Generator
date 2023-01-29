using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public Transform groundCheck;
    public float groundCheckThreshold = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCamera;
    public float lookSpeed = 1.0f;
    public float lookXLimit = 90.0f;

    CharacterController characterController;
    public Animator animator;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        animator.SetFloat("Walk", 0);
        animator.SetBool("Still", true);
        animator.SetBool("Grounded", true);

        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckThreshold, groundMask);

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * -Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * -Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        animator.SetFloat("Walk", -curSpeedX);

        if (curSpeedX != 0) {
            animator.SetBool("Still", false);
        } else {
            animator.SetBool("Still", true);
        }

        if (Input.GetButton("Jump") && canMove && isGrounded)
        {
            moveDirection.y = jumpSpeed;
            animator.SetBool("Jump", true);
        } else {
            moveDirection.y = movementDirectionY;
        }

        if (!Input.GetButton("Jump") && isGrounded) {

            animator.SetBool("Jump", false);
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
            animator.SetBool("Grounded", isGrounded);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    Gizmos.DrawWireSphere (groundCheck.position, groundCheckThreshold);
 }
}