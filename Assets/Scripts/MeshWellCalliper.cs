using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class MeshWellCalliper : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {

    }
    void CreateShape()
    {
        float[,] calliperReadings = { { 1f, 8.37f, 6.057f, 6.039f, 6.058f, 6.054f, 6.1145f},
                                { 5f,8.37f,6.057f,6.039f,6.058f,6.054f,6.1145f}/*,
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
                                { 6.18f,8.375f,6.019f,6.0232f,6.05f,6.048f,6.063f}*/};
        vertices = new Vector3[12];
        print("reading(No of records):" + calliperReadings.GetLength(0));
        print("length of arms:" + (calliperReadings.GetLength(1)-1));

        for (int cnt=0, index = 0;  cnt < calliperReadings.GetLength(0); cnt++)
     
            {

                //creating vertices using basic geometry for getting vertices for the hexagon
                //where as taking (depth) on Y axis . the first element is depth in the calliper reading.
            
                vertices.SetValue(new Vector3(calliperReadings[cnt, 1], calliperReadings[cnt, 0], 0), index++);//A
                vertices.SetValue(new Vector3(calliperReadings[cnt, 2] * Mathf.Cos(300 * Mathf.Deg2Rad), calliperReadings[cnt, 0], calliperReadings[cnt, 1] * Mathf.Sin(300 * Mathf.Deg2Rad)), index++);//F/*1,0,-1.7*/       
                vertices.SetValue(new Vector3(calliperReadings[cnt, 3] * Mathf.Cos(240 * Mathf.Deg2Rad), calliperReadings[cnt, 0], calliperReadings[cnt, 2] * Mathf.Sin(240 * Mathf.Deg2Rad)), index++);//E
                vertices.SetValue(new Vector3(calliperReadings[cnt, 4] * -1, calliperReadings[cnt, 0], 0), index++);//D
                vertices.SetValue(new Vector3(calliperReadings[cnt, 5] * Mathf.Cos(120 * Mathf.Deg2Rad), calliperReadings[cnt, 0], calliperReadings[cnt, 4] * Mathf.Sin(120 * Mathf.Deg2Rad)), index++);//C
                vertices.SetValue(new Vector3(calliperReadings[cnt, 6] * Mathf.Cos(60 * Mathf.Deg2Rad), calliperReadings[cnt, 0], calliperReadings[cnt, 5] * Mathf.Sin(60 * Mathf.Deg2Rad)), index++);//B

            }
        for (int i = 0; i < vertices.Length; i++)
        {
            print("index " + i + " " + vertices[i].ToString());
        }

        triangles = new int[]//always in clockwise direction
        {
            1,7,8,
            2,1,8
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
