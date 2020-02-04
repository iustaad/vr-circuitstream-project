using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    // Update is called once per 
    void Update()
    {

    }
    void CreateShape()
    {   
        float[] lengthArm = { 1f, 8.37f, 6.057f, 6.039f, 6.058f, 6.054f, 6.1145f };

        //for hexagon there should always be 7 vertices starting from the center
        vertices = new Vector3[7];

        //creating vertices for the hexagon
        //origin
        vertices.SetValue(new Vector3(0, lengthArm[0], 0), 0);
        vertices.SetValue(new Vector3(lengthArm[1], lengthArm[0], 0), 1);//A
        vertices.SetValue(new Vector3(lengthArm[2] * Mathf.Cos(300 * Mathf.Deg2Rad), lengthArm[0], lengthArm[1] * Mathf.Sin(300 * Mathf.Deg2Rad)), 2);//F/*1,0,-1.7*/       
        vertices.SetValue(new Vector3(lengthArm[3] * Mathf.Cos(240 * Mathf.Deg2Rad), lengthArm[0], lengthArm[2] * Mathf.Sin(240 * Mathf.Deg2Rad)), 3);//E
        vertices.SetValue(new Vector3(lengthArm[4] * -1, lengthArm[0], 0), 4);//D
        vertices.SetValue(new Vector3(lengthArm[5] * Mathf.Cos(120 * Mathf.Deg2Rad), lengthArm[0], lengthArm[4] * Mathf.Sin(120 * Mathf.Deg2Rad)), 5);//C
        vertices.SetValue(new Vector3(lengthArm[6] * Mathf.Cos(60 * Mathf.Deg2Rad), lengthArm[0], lengthArm[5] * Mathf.Sin(60 * Mathf.Deg2Rad)), 6);//B

        for (int i = 0; i < vertices.Length; i++)
        {
            print("index " + i + " " + vertices[i].ToString());
        }

        triangles = new int[]//always in clockwise direction
        {
            0,1,2,
            0,2,3,
            0,3,4,
            0,4,5,
            0,5,6,
            0,6,1
        };
               
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
