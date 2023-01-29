using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSwingRandomizer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] ceilings;
    public Material swingCeilingMat;
    System.Random rnd = new System.Random();
    void Start()
    {
        ceilings = GameObject.FindGameObjectsWithTag("sc");


    }
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (count == 0) {
            foreach(GameObject ceiling in ceilings) {

                //if a random number within range(min, max) is greater than #
                int number = rnd.Next(1, 10);
                //print(number);
                if(number > 3) {
                    ceiling.GetComponent<MeshRenderer>().material = swingCeilingMat;
                }

                count++;
            }
        }
    }
}
