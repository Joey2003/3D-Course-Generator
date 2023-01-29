using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongWay : MonoBehaviour
{
    public SectionSpawner spawner;
    public GameObject nextCheckpoint, currentCheckpoint;
    public GameObject wrongWayIcon;
    // Start is called before the first frame update
    void Start()
    {
        nextCheckpoint = spawner.checkpoints[0].transform.GetChild(0).gameObject;

        //set to one so wrong way is not called instantly (cant be -1)
        currentCheckpoint = spawner.checkpoints[0].transform.GetChild(0).gameObject;
    }

    bool goingWrongWay = false;
    // Update is called once per frame
    void Update()
    {
        if (goingWrongWay) {

            //wrong way code
            StartCoroutine(setWrongWay(true));
        } else {

            //time delay
            wrongWayIcon.GetComponent<FlashAlpha>().alpha = 0;
            wrongWayIcon.GetComponent<FlashAlpha>().cg.alpha = 0;
            wrongWayIcon.GetComponent<FlashAlpha>().currentAlpha = 0;
            StartCoroutine(setWrongWay(false));
        }
    }

    public int _nextCheckpoint = 0;    // target checkpoint (originally set to 0) (we target the second checkpoint on awake)
    public int _currentCheckpoint = -1;
    public int _beforeCheckpoint = -2;
    private void OnTriggerEnter(Collider collider) 
    {
        try
        {
            

        if (collider == spawner.checkpoints[_nextCheckpoint].transform.GetChild(0).gameObject.GetComponent<BoxCollider>()) {
        
            //we have reached out target checkpoint
            goingWrongWay = false;

            _currentCheckpoint ++;
            _nextCheckpoint  ++;
            _beforeCheckpoint  ++;


        
        } else if (collider == spawner.checkpoints[_beforeCheckpoint].transform.GetChild(0).gameObject.GetComponent<BoxCollider>()) {

            goingWrongWay = true;

            _currentCheckpoint--;
            _nextCheckpoint--;
            _beforeCheckpoint--;
        }
                }
        catch {}
        

    }
    IEnumerator setWrongWay(bool active)
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2);

        wrongWayIcon.SetActive(active);

    }
}
