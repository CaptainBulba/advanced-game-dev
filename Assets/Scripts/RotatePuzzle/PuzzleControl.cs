using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControl : MonoBehaviour
{
    public Transform[] puzzles;
    private int inCorrectPos;

    public void CheckPuzzlePos()
    {
        for(int i = 0; i < puzzles.Length; i++)
        {
            if (puzzles[i].rotation.z == 0) inCorrectPos++;
        }
        if (inCorrectPos == puzzles.Length) Debug.Log("You win!");
        if (inCorrectPos != puzzles.Length) inCorrectPos = 0;
    }
}
