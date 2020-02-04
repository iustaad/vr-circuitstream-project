using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellPolyInfo : MonoBehaviour
{
    public float pressure;
    public Material highlightMaterial;
    public Material origMaterial;
    public void highlight()
    {
        GetComponent<MeshRenderer>().material = highlightMaterial;
    }
    public void unHighlight()
    {
        GetComponent<MeshRenderer>().material = origMaterial;
    }
 
}
