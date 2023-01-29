using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionInit : MonoBehaviour
{
    private GameObject myObject;
    public Vector3 entrancePoint;
    public Transform exitPoint;

    private History history;

    //public Vector3 vertPos, exitVert;
    //Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {

        //vertices = mesh.vertices;
        history = GameObject.Find("History").GetComponent<History>();

    

        //GameObject.Find(this.gameObject.name +"/entrance point").transform.localPosition = new Vector3(0,0,0);
        for(int a = 0; a < this.gameObject.transform.GetChild(0).transform.childCount; a++) {

            if(this.gameObject.transform.GetChild(0).transform.GetChild(a).name.Equals("entrance point")) {
                this.gameObject.transform.GetChild(0).transform.GetChild(a).transform.localPosition = new Vector3(0,0,0);
                entrancePoint = this.gameObject.transform.GetChild(0).transform.GetChild(a).transform.position;
            } else if (this.gameObject.transform.GetChild(0).transform.GetChild(a).name.Equals("exit point")) {
                exitPoint = this.gameObject.transform.GetChild(0).transform.GetChild(a);
                history.setPreviousExitPoint(exitPoint);
//                print(this.gameObject.name + " " + exitPoint.position);
            }
        }

       
    }
}
