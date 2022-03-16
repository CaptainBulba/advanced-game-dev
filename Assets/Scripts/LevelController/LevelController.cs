using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject background;
    public GameObject[] puzzles;
    public GameObject buttonsGroup;

    public int TotalPuzzles;

    private int completedPuzzles = 0;
    private int currentLevel;

    public enum GameActions
    {
        RotatePuzzle,
        NextRoom,
        GuessNumberPuzzle
    }

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void LaunchMainScreen()
    {
        Debug.Log("Returned to main level");
        completedPuzzles++;
        buttonsGroup.SetActive(true);
        background.SetActive(true);

        for (int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].SetActive(false);
        }
    }

    public void InitiateButton(string actionName)
    {
        if (actionName == GameConstants.puzzleRotation)
        {
            buttonsGroup.SetActive(false);
            background.SetActive(false);

            // Activate RotatePuzzle
            puzzles[0].SetActive(true);
        }

        if (actionName == GameConstants.puzzleGuessNum)
        {
            buttonsGroup.SetActive(false);
            background.SetActive(false);

            // Activate RotatePuzzle
            puzzles[1].SetActive(true);

        }

        if (actionName == GameConstants.puzzleLock)
        {
            buttonsGroup.SetActive(false);
            background.SetActive(false);

            // Activate Guess Puzzle
            puzzles[2].SetActive(true);
        }

        if (actionName == GameConstants.actionNextRoom)
        {
            if (completedPuzzles >= TotalPuzzles) SceneManager.LoadScene(currentLevel + 1);
            else Debug.Log("You have not completed all puzzles");            
        }
    }
}
