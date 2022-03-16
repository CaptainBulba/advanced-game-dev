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

<<<<<<< Updated upstream:Assets/Scripts/LevelController.cs
=======
    public enum GameActions
    {
        RotatePuzzle,
        NextRoom,
        GuessNumberPuzzle,
        SliddingPuzzle
    }

>>>>>>> Stashed changes:Assets/Scripts/LevelController/LevelController.cs
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
<<<<<<< Updated upstream:Assets/Scripts/LevelController.cs
        if(actionName == "rotate_puzzle")
=======
        //if (actionName == GameConstants.puzzleGuessNum)
        if (actionName == GameConstants.puzzleRotation)
>>>>>>> Stashed changes:Assets/Scripts/LevelController/LevelController.cs
        {
            buttonsGroup.SetActive(false);
            background.SetActive(false);

            // Activate RotatePuzzle
            puzzles[0].SetActive(true);
        }

        if (actionName == "next_room")
        {
            if (completedPuzzles >= TotalPuzzles) SceneManager.LoadScene(currentLevel + 1);
            else Debug.Log("You have not completed all puzzles");            
        }
    }
}
