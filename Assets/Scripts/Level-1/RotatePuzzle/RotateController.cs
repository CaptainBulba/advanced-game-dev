using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public Transform[] puzzles;
    private int inCorrectPos;

    private LevelController levelController;
    private GameObject puzzleButton;

    void Start()
    {
        levelController = GetComponent<PrefabSettings>().GetLevelController();
        puzzleButton = GetComponent<PrefabSettings>().GetButton();
    }

    public void CheckPuzzlePos()
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (puzzles[i].rotation.z == 0) inCorrectPos++;
        }
        if (inCorrectPos == puzzles.Length) levelController.LaunchMainScreen(puzzleButton);
        if (inCorrectPos != puzzles.Length) inCorrectPos = 0;
    }
}
