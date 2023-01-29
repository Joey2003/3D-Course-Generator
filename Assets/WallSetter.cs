using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetter : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] groundWalls;
    public Transform golfGreen;
    public Material dirtMat;
    private Material[] dirtMat_1;
    private Material[] dirtMat_2;

    public Material dirtCeilingMat;
    private Material[] dirtCeilingMat_1;
    private Material[] dirtCeilingMat_2;

    public List<GameObject> dirtWalls;
    public List<GameObject> dirtCeilings;
    private void Start() {

        dirtMat_1 = new Material[1] {dirtMat};
        dirtMat_2 = new Material[2] {dirtMat, dirtMat};

        dirtCeilingMat_1 = new Material[1] {dirtCeilingMat};
        dirtCeilingMat_2 = new Material[2] {dirtCeilingMat, dirtCeilingMat};
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))) {

            //layer 6: Sections  |  layer 11: SC
            if (obj.layer == 6) { 
                if (obj.transform.position.y-1 < golfGreen.position.y && obj.GetComponent<MeshRenderer>() != null) {
                    dirtWalls.Add(obj);
                }
            }

            if (obj.layer == 11) {
                if (obj.transform.position.y < golfGreen.position.y && obj.GetComponent<MeshRenderer>() != null) {
                    dirtCeilings.Add(obj);
                }
            }
        }
    }
    bool loop = true;
    bool loop_2 = true;

    // Update is called once per frame
    void Update()
    {
        int count = 0;

        if (loop) {
            for (int a = 0; a < dirtWalls.Count; a++) {
                count = 0;
                if (dirtWalls[a].GetComponent<MeshRenderer>().materials[0] == dirtMat &&
                    dirtWalls[a].GetComponent<MeshRenderer>().materials[dirtWalls[a].GetComponent<MeshRenderer>().materials.Length-1] == dirtMat) {

                    count++;
                    continue;
                }
                if (count == dirtWalls.Count) {

                    loop = false;
                    break;
                }
                if(dirtWalls[a].GetComponent<MeshRenderer>().materials.Length == 2) {
                    dirtWalls[a].GetComponent<MeshRenderer>().materials = dirtMat_2;
                } else if (dirtWalls[a].GetComponent<MeshRenderer>().materials.Length == 1) {
                    dirtWalls[a].GetComponent<MeshRenderer>().materials = dirtMat_1;
                }
            }
        }

        count = 0;
        
        if (loop_2) {
            for (int a = 0; a < dirtCeilings.Count; a++) {
                count = 0;
                if (dirtCeilings[a].GetComponent<MeshRenderer>().materials[0] == dirtCeilingMat &&
                    dirtCeilings[a].GetComponent<MeshRenderer>().materials[dirtCeilings[a].GetComponent<MeshRenderer>().materials.Length-1] == dirtCeilingMat) {

                    count++;
                    continue;
                }
                if (count == dirtCeilings.Count) {

                    loop = false;
                    break;
                }
                if(dirtCeilings[a].GetComponent<MeshRenderer>().materials.Length == 2) {
                    dirtCeilings[a].GetComponent<MeshRenderer>().materials = dirtCeilingMat_2;
                } else if (dirtCeilings[a].GetComponent<MeshRenderer>().materials.Length == 1) {
                    dirtCeilings[a].GetComponent<MeshRenderer>().materials = dirtCeilingMat_1;
                }
            }
        }
    }
}
