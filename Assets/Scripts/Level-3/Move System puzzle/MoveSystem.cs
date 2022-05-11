using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    public GameObject keycolor;
    private bool moving;

    private float startPosx;
    private float startPosy;

    private Vector3 resetpos;
    public bool doneornot;


    void Start()
    {
        resetpos = this.transform.localPosition;


    }

    void Update()
    {

        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosx, mousePos.y - startPosy, this.gameObject.transform.localPosition.z);
        }

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosx = mousePos.x - this.transform.localPosition.x;
            startPosy = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;
        doneornot = false;

        if (Mathf.Abs(this.transform.localPosition.x - keycolor.transform.localPosition.x) <= 0.5 &&
            Mathf.Abs(this.transform.localPosition.y - keycolor.transform.localPosition.y) <= 0.5)
        {
            this.transform.localPosition = new Vector3(keycolor.transform.localPosition.x, keycolor.transform.localPosition.y, keycolor.transform.localPosition.z);
            doneornot = true;
        }

        else
        {
            this.transform.localPosition = new Vector3(resetpos.x, resetpos.y, resetpos.z);
        }
    }

}
