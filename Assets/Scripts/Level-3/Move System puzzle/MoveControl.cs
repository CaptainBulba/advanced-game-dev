using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public GameObject keyholes;
    public GameObject keys;

    public bool isit;

    private void Update()
    {
        if(isit == false)
        {
            keyholes.SetActive(false);
            keys.SetActive(false);
        }
        if(isit == true)
        {
            keyholes.SetActive(true);
            keys.SetActive(true);
        }
        else
        {
            isit = false;
        }
    }
}
