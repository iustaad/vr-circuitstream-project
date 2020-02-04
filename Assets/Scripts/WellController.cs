using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellController : MonoBehaviour
{
    public GameObject well;
    
    public void EnableWell()
    {
        well.SetActive(true);
    }

}
