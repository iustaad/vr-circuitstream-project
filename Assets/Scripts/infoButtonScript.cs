using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoButtonScript : MonoBehaviour
{
    string infoButton;
    public GameObject buttonName;
    public GameObject panel;

    
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                infoButton = hit.transform.name;
                if (infoButton == buttonName.transform.name)
                
                {                                   
                        panel.SetActive(true);                       
                }
            }
        }
    }
}
