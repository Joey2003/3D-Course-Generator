using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{

    public Transform character;
    private Mesh mesh;
    public Vector3[] verts;
    public List<Vector3> circle;
    public Vector3 lastPos;
    public float threshold;
    // Start is called before the first frame update

    void Awake() {

        lastPos = transform.InverseTransformPoint(character.position);
    }
    void Start()
    {
        mesh = transform.gameObject.GetComponent<MeshFilter>().sharedMesh;

        verts = mesh.vertices;

        for ( int a = 0; a < verts.Length; a++) {
            if ((verts[a].x < transform.InverseTransformPoint(character.position).x + threshold && verts[a].x > transform.InverseTransformPoint(character.position).x - threshold) &&
                (verts[a].z < transform.InverseTransformPoint(character.position).z + threshold && verts[a].z > transform.InverseTransformPoint(character.position).z - threshold)) {

                    circle.Add(verts[a] - transform.InverseTransformPoint(character.position));
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPos = transform.InverseTransformPoint(character.position) - lastPos;
        int count = 0;
        for ( int a = 0; a < verts.Length; a++) {

            if ((verts[a].x < transform.InverseTransformPoint(character.position).x + threshold && verts[a].x > transform.InverseTransformPoint(character.position).x - threshold) &&
                (verts[a].z < transform.InverseTransformPoint(character.position).z + threshold && verts[a].z > transform.InverseTransformPoint(character.position).z - threshold)) {

                //verts[a] = (character.position + circle[a]);
                verts[a] = new Vector3(transform.InverseTransformPoint(character.position).x + circle[count].x, verts[a].y, transform.InverseTransformPoint(character.position).z + circle[count].z);
                count++;
            }
        }

        mesh.vertices = verts;
        //19 36 38 34 32 40 30 49 36 52 58 23 59 22 16 17 18 12 3
        lastPos = transform.InverseTransformPoint(character.position);
    }
}
