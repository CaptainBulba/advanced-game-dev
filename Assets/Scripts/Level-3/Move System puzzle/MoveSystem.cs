using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    public GameObject keycolor;
    private bool moving;

    private float startPosx;
    private float startPosy;
    public static int count;

    private Vector3 resetpos;
    public bool doneornot;


    void Start()
    {
        resetpos = this.transform.position;


    }

    void Update()
    {

        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

<<<<<<< HEAD
            this.gameObject.transform.position = new Vector3(mousePos.x - startPosx, mousePos.y - startPosy, this.gameObject.transform.position.z);
=======
            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, this.gameObject.transform.position.z);
>>>>>>> 9df0974d12ed4b523c527f8c2f2d25dda5f6ad07
              
        }

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

<<<<<<< HEAD
            startPosx = mousePos.x - this.transform.position.x;
            startPosy = mousePos.y - this.transform.position.y;
=======
            startPosx = mousePos.x - this.transform.position.x/2;
            startPosy = mousePos.y - this.transform.position.y/2;
>>>>>>> 9df0974d12ed4b523c527f8c2f2d25dda5f6ad07

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;
        doneornot = false;

        if (Mathf.Abs(this.transform.position.x - keycolor.transform.position.x) <= 0.5 &&
            Mathf.Abs(this.transform.position.y - keycolor.transform.position.y) <= 0.5)
        {
            this.transform.position = new Vector3(keycolor.transform.position.x, keycolor.transform.position.y, keycolor.transform.position.z);
            doneornot = true;

            count++;
            Debug.Log("count" +count);
        }

        else
        {
            this.transform.position = new Vector3(resetpos.x, resetpos.y, resetpos.z);
        }
    }

}
