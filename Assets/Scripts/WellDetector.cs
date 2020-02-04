using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellDetector : MonoBehaviour
{
    private WellPolyInfo selectedInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.current.transform.position, Camera.current.transform.forward, out hitInfo)){
            if (hitInfo.transform.GetComponent<WellController>())
            {
                hitInfo.transform.GetComponent<WellController>().EnableWell();
            }
            if (hitInfo.transform.GetComponent<WellPolyInfo>())
            {
                selectedInfo = hitInfo.transform.GetComponent<WellPolyInfo>();
                selectedInfo.highlight();
            }
            else if (selectedInfo != null)
            {
                
                selectedInfo.unHighlight();
            }
        }
        else if (selectedInfo != null)
        {

            selectedInfo.unHighlight();
        }
    }
}
