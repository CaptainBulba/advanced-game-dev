using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject background;
    public GameObject[] puzzles;
    public GameObject buttonsGroup;

    public void LaunchMainScreen()
    {
        buttonsGroup.SetActive(true);
        background.SetActive(true);

        for (int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].SetActive(false);
        }
    }

    public void InitiateButton(string actionName)
    {
        if(actionName == "rotate_puzzle")
        {
            buttonsGroup.SetActive(false);
            background.SetActive(false);

            // Activate RotatePuzzle
            puzzles[0].SetActive(true);
         }
    }
}
