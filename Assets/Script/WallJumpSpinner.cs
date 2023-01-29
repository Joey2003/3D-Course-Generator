using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpSpinner : MonoBehaviour
{

    public GameObject[] wallJumps;
    public float rotationSpeed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        wallJumps = GameObject.FindGameObjectsWithTag("wj");

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject walljump in wallJumps)
        {
            int index = System.Array.IndexOf(wallJumps, walljump);
            if (index % 2 == 0) {
                walljump.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
                /*rotation.Set(walljump.transform.rotation.x,
                                                walljump.transform.rotation.y + 20,
                                                walljump.transform.rotation.z,
                                                walljump.transform.rotation.w);
                                                print("Clockwise");*/
            } else {
                walljump.transform.Rotate(Vector3.up * (-rotationSpeed * Time.deltaTime));
                /*rotation.Set(walljump.transform.rotation.x,
                                                walljump.transform.rotation.y - 20,
                                                walljump.transform.rotation.z,
                                                walljump.transform.rotation.w);
                                                print("Counter-Clockwise");*/
            }


        }
    }
}
