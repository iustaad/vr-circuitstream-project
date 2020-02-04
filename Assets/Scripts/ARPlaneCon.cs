using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlaneCon : MonoBehaviour
{
    ArPrefabSpawner prefabSpawner;
    MeshRenderer meshRenderer;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();//its only search on the game object
        meshRenderer = GetComponent<MeshRenderer>();
        prefabSpawner = FindObjectOfType<ArPrefabSpawner>();//all hierarchy
    }

    // Update is called once per frame
    void Update()
    {
        if (prefabSpawner.HasInstantiated())
        {
            if (meshRenderer.enabled)
            {
                meshRenderer.enabled = false;
            }
            if (lineRenderer.enabled)
            {
               lineRenderer.enabled = false;
            }
        }
    }
}
