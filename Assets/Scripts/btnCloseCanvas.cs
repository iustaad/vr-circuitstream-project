using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnCloseCanvas : MonoBehaviour
{
    public GameObject Panel;

    // Start is called before the first frame update
    public void ClosePanel()
    {
        bool isActive = Panel.activeSelf;
        Panel.SetActive(!isActive);
    }
}
