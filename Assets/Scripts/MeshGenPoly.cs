using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenPoly : MonoBehaviour
{
    public GameObject polygonPrefab;
    Vector3[] vertices;
    int[] triangles;
    public Material mat;

    void Start()
    {
        /*float[] lengthArm = { 1f, 8.37f, 6.057f, 6.039f, 6.058f, 6.054f, 6.1145f };
        CreateShape(lengthArm);

        float[] lengthArm2 = { 5f, 8.37f, 6.057f, 6.039f, 6.058f, 6.054f, 6.1145f };
        CreateShape(lengthArm2);*/

        float[,] calliperReadings = { 
                                { 0f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 0.5f, 8.37f, 6.057f, 6.039f, 6.058f, 6.054f, 6.1145f},
                                { 1.2f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 1.3f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 1.4f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 1.5f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 1.6f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 1.7f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 1.8f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 1.9f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.1f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.2f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.3f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.4f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.5f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.6f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.7f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.8f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.9f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 2.9f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 3f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 3.01f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 3.02f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 3.3f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f},
                                { 3.32f,8.37f,6.057f,6.039f,6.058f,6.054f,6.114f},
                                { 3.33f,8.37f,6.057f,6.05f,6.058f,6f,6f},
                                { 3.34f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.34f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.35f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.4f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.413f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.43f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.45f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.56f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.59f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.63f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.65f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.7f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.72f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.79f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.8f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.85f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.89f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 3.9f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.1f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.12f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.13f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.15f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.16f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.17f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.2f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.23f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.29f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.2f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.3f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.4f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.5f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.6f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.7f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.8f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 4.9f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.01f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.12f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.13f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.4f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.5f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.6f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.7f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.8f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 5.9f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.1f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.12f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.13f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.14f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.15f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.16f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.17f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.18f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.19f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.18f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f},
                                { 6.18f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f}};
        
        for (int depthReadings=0;depthReadings< calliperReadings.GetLength(0); depthReadings++ )
        {
            float[] lengthArm = new float[calliperReadings.GetLength(1)];
            // copy elemements to destination array
            for (int i = 0; i < calliperReadings.GetLength(1); i++)
            {
                lengthArm[i] = (float) calliperReadings.GetValue(depthReadings, i);
            }
            CreateShape(lengthArm);
        }
    }

    void CreateShape(float[] lengthArm)
    {
        Vector3 heightOffset = new Vector3(0, lengthArm[0]* 0.3048f, 0); // Height delta also conversting from foot to meter.

        GameObject polygonInstance = Instantiate(polygonPrefab, transform.position, Quaternion.identity, transform);
       
            //for hexagon there should always be 7 vertices starting from the center
        vertices = new Vector3[7];

        //creating vertices for the hexagon
        //origin
        vertices.SetValue(new Vector3(0, 0, 0), 0);
        vertices.SetValue(new Vector3(lengthArm[1], 0, 0), 1);//A
        vertices.SetValue(new Vector3(lengthArm[2] * Mathf.Cos(300 * Mathf.Deg2Rad), 0, lengthArm[1] * Mathf.Sin(300 * Mathf.Deg2Rad)), 2);//F/*1,0,-1.7*/       
        vertices.SetValue(new Vector3(lengthArm[3] * Mathf.Cos(240 * Mathf.Deg2Rad), 0, lengthArm[2] * Mathf.Sin(240 * Mathf.Deg2Rad)), 3);//E
        vertices.SetValue(new Vector3(lengthArm[4] * -1, 0, 0), 4);//D
        vertices.SetValue(new Vector3(lengthArm[5] * Mathf.Cos(120 * Mathf.Deg2Rad), 0, lengthArm[4] * Mathf.Sin(120 * Mathf.Deg2Rad)), 5);//C
        vertices.SetValue(new Vector3(lengthArm[6] * Mathf.Cos(60 * Mathf.Deg2Rad), 0, lengthArm[5] * Mathf.Sin(60 * Mathf.Deg2Rad)), 6);//B

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

        UpdateMesh(polygonInstance, heightOffset);
    }

    void UpdateMesh(GameObject polyInst, Vector3 heightOffset)
    {
        Mesh mesh = polyInst.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        polyInst.GetComponent<MeshRenderer>().material = mat;

        polyInst.transform.position -= heightOffset; // Lower it down from the parent position
    }
}
