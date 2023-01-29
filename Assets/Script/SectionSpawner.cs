using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionSpawner : MonoBehaviour
{
    public SectionChooser chooser;
    public SectionMatrix matrix;
    public SectionChooser.MapSection currentSec;
    public GameObject startingSection, history;
    public Transform startingPoint;

    public GameObject obstacleManager;
    Vector3 spawnPoint;
    public List<GameObject> checkpoints = new List<GameObject>();
    public int checkPoint_interval = 3;

    public static int NORTH = 1;
    public static int EAST = 2;
    public static int SOUTH = 3;
    public static int WEST = 4;
    public static int UP = 5;
    public static int DOWN = 6;

    private void Awake() {
        
        checkpoints.Add(startingSection);
    }
    // Start is called before the first frame update
    void Start()
    {

        currentSec = new SectionChooser.MapSection(startingSection, 1, 1, false, -1);//chooser.GetSection(0);
        currentSec.setCoords(2,2,3);

        spawnPoint = startingPoint.position;
    }

    // Update is called once per frame
    bool stop = false;
    int count = 0;
    void Update()
    {
        SectionChooser.MapSection nextSection = null;


        bool execute = true;
        if (!stop) {
            while(execute) {
                SectionChooser.MapSection sec = null;

                try
                {
                    sec = chooser.getValidSection(currentSec, matrix);
                }
                catch {}

                if (sec == null) {

                    obstacleManager.SetActive(true);
                    gameObject.SetActive(false);
                }



                if (sec != null) {
                    nextSection = sec;
                    execute = false;

                    SectionChooser.MapSection prevSection = currentSec;
                    currentSec = nextSection;

                    int x = 0;
                    int y = 0;
                    int z = 0;
                    
                    if(currentSec.entrance_direction == NORTH && matrix.currentPos_z < matrix.matrixSize-1) {z++;}
                    else if(currentSec.entrance_direction == EAST && matrix.currentPos_x < matrix.matrixSize-1) {x++;}
                    else if(currentSec.entrance_direction == SOUTH && matrix.currentPos_z >= 0) {z--;}
                    else if(currentSec.entrance_direction == WEST && matrix.currentPos_x >= 0) {x--;}
                    else if(currentSec.entrance_direction == UP && matrix.currentPos_y >= 0) {y--;}
                    else if(currentSec.entrance_direction == DOWN && matrix.currentPos_y < matrix.matrixSize-1) {y++;}
                    
                    int currentPos_x = matrix.currentPos_x+x;
                    int currentPos_y = matrix.currentPos_y+y;
                    int currentPos_z = matrix.currentPos_z+z;

                    if (!matrix.matrix[currentPos_y, currentPos_x, currentPos_z]) {

                        matrix.matrix[currentPos_y, currentPos_x, currentPos_z] = true;
                    }

                    Vector3 prevExitPoint = history.GetComponent<History>().getPreviousExitPoint().position;//prevSection.gameObject.transform.GetChild(1).position;

                    GameObject section = Instantiate(nextSection.gameObject, prevExitPoint
                                                    , nextSection.gameObject.transform.rotation);
                   // print(count+1);
                    if ((count+1) % checkPoint_interval == 0) {

                        checkpoints.Add(section);
                    }
                    nextSection.setCoords(currentPos_x, currentPos_y, currentPos_z);
                    //print(currentSec.gameObject.name + " xyz: " + nextSection.getCoords());

                    matrix.currentPos_y = currentPos_y;
                    matrix.currentPos_x = currentPos_x;
                    matrix.currentPos_z = currentPos_z;

                    //count keeps track of how many sections spawned
                    count++;

                } else {
                    //end or section production
                    stop = true;
                    break;
                }
            }
        }
    }
}
