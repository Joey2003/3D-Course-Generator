using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float lookSpeed = 3;
    public Transform playerBody;
    private Vector2 rotation = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.y = Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y");


        Camera.main.transform.localRotation = Quaternion.Euler(Mathf.Clamp(rotation.x * lookSpeed, -90f, 90f), 0f, 0);
        playerBody.Rotate(Vector3.up * (rotation.y * lookSpeed));
    }


    public void LookUpdate() // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {

    }
}
