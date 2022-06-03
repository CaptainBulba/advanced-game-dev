using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePuzzle : MonoBehaviour
{
    public RotateController rotateController;
    private void OnMouseDown()
    {
        transform.Rotate(0f, 0f, 90f);
        rotateController.CheckPuzzlePos();
    }
}
