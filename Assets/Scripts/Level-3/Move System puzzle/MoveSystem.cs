using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    public GameObject keyColor;
    private bool moving;

    private float startPosx;
    private float startPosy;
    public static int count;

    private Vector3 resetPos;
    public bool done;

    void Start()
    {
        count = 0;
        resetPos = this.transform.position;
    }

    void Update()
    {
        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.position = new Vector3(mousePos.x - startPosx, mousePos.y - startPosy, this.gameObject.transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosx = mousePos.x - this.transform.position.x;
            startPosy = mousePos.y - this.transform.position.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(this.transform.position.x - keyColor.transform.position.x) <= 0.5 &&
            Mathf.Abs(this.transform.position.y - keyColor.transform.position.y) <= 0.5)
        {
            this.transform.position = keyColor.transform.position;

            if (!done)
            {
                count++;
                done = true;
            }
        }
        else
        {
            this.transform.position = resetPos;
        }

        moving = false;
    }
}
