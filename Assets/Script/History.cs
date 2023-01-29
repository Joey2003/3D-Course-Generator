using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{

    private Transform prevExitPointPos;
    public Transform firstExitPoint;

    // Start is called before the first frame update
    void Start()
    {
        prevExitPointPos = firstExitPoint;
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPreviousExitPoint(Transform point)
    {
        prevExitPointPos = point;
    }

    public Transform getPreviousExitPoint()
    {

        //print(prevExitPointPos.position);
        return prevExitPointPos;
    }

    public GameObject getPreviousPointObj()
    {
        return prevExitPointPos.gameObject;
    }
}
