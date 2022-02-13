using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePuzzle : MonoBehaviour
{
    public PuzzleControl puzzleControl;
    private void OnMouseDown()
    {
        transform.Rotate(0f, 0f, 90f);
        puzzleControl.CheckPuzzlePos();
    }
}
