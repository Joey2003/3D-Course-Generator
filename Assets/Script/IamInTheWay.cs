using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IamInTheWay : MonoBehaviour
{
    [SerializeField] private GameObject solidBody, transparentBody;
    private void Awake() {
        
        solidBody = gameObject.transform.GetChild(2).gameObject;
        transparentBody = gameObject.transform.GetChild(1).gameObject;
    }


    public void ShowTransparent() {


        solidBody.SetActive(false);
        transparentBody.SetActive(true);
    }

    public void ShowSolid() {

        solidBody.SetActive(true);
        transparentBody.SetActive(false);
    }
}
