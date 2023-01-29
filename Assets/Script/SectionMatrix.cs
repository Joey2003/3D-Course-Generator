using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionMatrix : MonoBehaviour
{
    public int matrixSize = 10;
    public bool[,,] matrix;

    public int currentPos_y, currentPos_x, currentPos_z;

    // Start is called before the first frame update
    void Start()
    {
        matrix = new bool[matrixSize, matrixSize, matrixSize];

        matrix[1,1,1] = true;
        matrix[1,2,1] = true;
        matrix[1,3,1] = true;
        matrix[1,1,2] = true;//    / \
        matrix[1,2,2] = true;//  /   /\
        matrix[1,3,2] = true;// |\ /  |   CUBE.
        matrix[2,1,1] = true;// \ |  /
        matrix[2,2,1] = true;//  \|/
        matrix[2,3,1] = true;//      This is the center-ish of a 5x5x5 matrix
        matrix[2,1,2] = true;//      occupied by the starting base that is 2x3x2
        matrix[2,2,2] = true;
        matrix[2,3,2] = true;

        matrix[2,2,3] = true;//is the index of the first section
        currentPos_y = 2;
        currentPos_x = 2;
        currentPos_z = 3;
    }



    public void setCurrentPos(int y, int x, int z) {
        this.currentPos_y = y;
        this.currentPos_x = x;
        this.currentPos_z = z;
    }

    public string getCurrentPos() {
        return "[" + currentPos_y + "," + currentPos_x + "," + currentPos_z + "]";
    }
}
