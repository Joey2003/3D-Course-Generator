using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabbing : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement1 pm;
    public Transform orientation;
    public Transform cam;
    private Rigidbody rb;

    [Header("Ledge Grabbing")]
    public float moveToLedgeSpeed;
    public float maxLedgeGrabDistance;

    public float maxTimeOnLedge;
    private float timeOnLedge;

    public bool holding;

    [Header("Ledge Jumping")]
    public KeyCode jumpKey = KeyCode.Space;
    public float ledgeJumpForwardForce;
    public float ledgeJumpUpwardForce;

    [Header("Ledge Detection")]
    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask whatIsLedge;

    private Transform lastLedge;
    private Transform currLedge;

    private RaycastHit ledgeHit;

    [Header("Exiting")]
    public bool exitingLedge;
    public float exitLedgeTime;
    public float exitLedgeTimer;
    Vector3 directionToLedge = new Vector3 (0f,0f,0f);

    private void Start() {
        pm = gameObject.GetComponent<PlayerMovement1>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        LedgeDetection();
        SubStateMachine();

        try {
            directionToLedge = currLedge.InverseTransformPoint(transform.position) * 100;
        } catch {}

    }

    private void SubStateMachine()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool anyInputKeyPressed = -horizontalInput != 0 || -verticalInput != 0;

        // SubState 1 - Holding onto ledge
        if (holding)
        {
            FreezeRigidbodyOnLedge();

            timeOnLedge += Time.deltaTime;

            if (timeOnLedge > maxTimeOnLedge && anyInputKeyPressed) ExitLedgeHold();

            if (Input.GetKeyDown(jumpKey)) LedgeJump();
        }

        // Substate 2 - Exiting Ledge
        else if (exitingLedge)
        {
            if (exitLedgeTimer > 0) exitLedgeTimer -= Time.deltaTime;
            else exitingLedge = false;
        }
    }
    

    private void LedgeDetection()
    {
        bool ledgeDetected = Physics.SphereCast(cam.position, ledgeSphereCastRadius, cam.forward, out ledgeHit, ledgeDetectionLength, whatIsLedge);

        if (!ledgeDetected) {

            return; 
        } else {

        }

        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);

        if (ledgeHit.transform == lastLedge) return;

        if (distanceToLedge < maxLedgeGrabDistance && !holding) {
            EnterLedgeHold();
        }
    }

    private void LedgeJump()
    {
        ExitLedgeHold();

        Invoke(nameof(DelayedJumpForce), 0.05f);
    }

    private void DelayedJumpForce()
    {
        Vector3 forceToAdd = cam.forward * ledgeJumpForwardForce + orientation.up * ledgeJumpUpwardForce;
        rb.velocity = Vector3.zero;
        rb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    private void EnterLedgeHold()
    {
        holding = true;

        pm.unlimited = true;
        pm.restricted = true;

        currLedge = ledgeHit.transform;
        lastLedge = ledgeHit.transform;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }
    bool touchingLedge = false;
    public float wallJumpWidth;
    private void FreezeRigidbodyOnLedge()
    {
        rb.useGravity = false;

        float distanceToLedge = Vector3.Distance(transform.position, currLedge.position);
        // Move player towards ledge
        

        // Hold onto ledge
       if(//((directionToLedge.x < wallJumpWidth && directionToLedge.x > -wallJumpWidth) && 
            //(directionToLedge.y < wallJumpWidth && directionToLedge.y > -wallJumpWidth)) &&
            touchingLedge)
        {
            if (!pm.freeze) pm.freeze = true;
            if (pm.unlimited) pm.unlimited = false;
        }

        // Exiting if something goes wrong
        if (distanceToLedge > maxLedgeGrabDistance) ExitLedgeHold();
    }

    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.layer == 12) {
            touchingLedge = true;
        }

    }

    private void ExitLedgeHold()
    {
        exitingLedge = true;
        exitLedgeTimer = exitLedgeTime;

        holding = false;
        timeOnLedge = 0f;

        touchingLedge = false;

        pm.restricted = false;
        pm.freeze = false;

        rb.useGravity = true;

        StopAllCoroutines();
        Invoke(nameof(ResetLastLedge), 1f);
    }

    private void ResetLastLedge()
    {
        lastLedge = null;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;

        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (new Ray(cam.position, cam.forward).GetPoint(ledgeDetectionLength), ledgeSphereCastRadius);
 
    }
}
