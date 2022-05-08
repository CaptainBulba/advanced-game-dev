using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessNumItem : MonoBehaviour
{
    public GameObject displayObject;
    private GameObject canvasObject; 
   
    void Start()
    {
        canvasObject = GameObject.Find("Canvas"); 
    }

    public void UseItem()
    {
        Instantiate(displayObject, canvasObject.transform, false);
    }

}

