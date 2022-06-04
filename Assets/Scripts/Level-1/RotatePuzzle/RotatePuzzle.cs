using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePuzzle : MonoBehaviour
{
    public RotateController rotateController;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.Rotate(0f, 0f, 90f);
            rotateController.CheckPuzzlePos();
        }
    }
}
