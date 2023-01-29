using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setInactive : MonoBehaviour
{

    public Transform character;
    public List<GameObject> sections;
    public float threshold, height;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Section")) {
            sections.Add(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("wj")) {
            sections.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in sections) {
            if(obj.transform.position.x <= character.position.x - threshold
            || obj.transform.position.x >= character.position.x + threshold
            || obj.transform.position.z >= character.position.z + threshold
            || obj.transform.position.z <= character.position.z - threshold) 
            {
                obj.SetActive(false);
            } else if (obj.transform.position.y <= character.position.y - height
                    || obj.transform.position.y >= character.position.y + height) 
            {
                obj.SetActive(false);
            }else {

                obj.SetActive(true);
            }
        }
    }
}
