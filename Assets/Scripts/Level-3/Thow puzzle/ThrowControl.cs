using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowControl : MonoBehaviour
{
    public GameObject wall;
    public GameObject bucket;
    public GameObject tobj;
    public GameObject swit;
    private ObjDirection tobjs;


    public void Update()
    {
        CheckStatus();
    }

    public void CheckStatus() 
    {
        tobjs = tobj.GetComponent<ObjDirection>();

        if (tobjs.finished == true) 
        {
            wall.SetActive(false);
            bucket.SetActive(false); 
            swit.SetActive(false); 
            tobj.SetActive(false);
        }
        if(tobjs.finished == false)
        {
            wall.SetActive(true);
            bucket.SetActive(true);
            swit.SetActive(true);
            tobj.SetActive(true);
        }
    }
}
