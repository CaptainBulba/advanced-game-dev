using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePuzzle : MonoBehaviour
{
    public RotateController rotateController;

    void OnMouseOver()
    {
        //mouse left click
        if (Input.GetMouseButtonDown(0))
        {
            //rotate along the z-axis
            transform.Rotate(Vector3.forward * 90);

            rotateController.CheckPuzzlePos();
        }

        //mouse right click
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(Vector3.forward * -90);
            rotateController.CheckPuzzlePos();
        }
    }
}
