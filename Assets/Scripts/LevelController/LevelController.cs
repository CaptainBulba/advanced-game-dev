using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject background;

    //Will contain the puzzle prefabs
    public GameObject[] puzzles;

    public GameObject buttonsGroup;
    
    //Will contain an array of all the buttons within the level
    public Button[] puzzleButtons;

    public int TotalPuzzles;

    private int completedPuzzles = 0;
    private int currentLevel;



    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        //Get the number of puzzles from the enum
        int numLevelOneActions = System.Enum.GetNames(typeof(LevelOneActions)).Length;
        
        /*
        //Except moving to the next level, Currently it is done manual in the unity editor
        puzzles = new GameObject[numLevelOneActions-1];

        for (int i = 0; i < numLevelOneActions - 1; i++)
        {
            Debug.Log((LevelOneActions)i);

            puzzles[i] = Instantiate("RotatePuzzle", new Vector3(0, 0, 0), Quaternion.identity);
        }
        */ 

        //Define all buttons within the level
        //puzzleButtons = new Button[numLevelOneActions];

        
 
        

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

    public void SetupPuzzleButtons()
    {

    }

    public void LaunchPuzzle(int actionIndex)
    {
        //actionButton.ge
        buttonsGroup.SetActive(false);
        //background.SetActive(false);

        // Activate RotatePuzzle
        puzzles[actionIndex].SetActive(true);

    }
    public void InitiateButton(string actionName)
    {/*
        if (actionName == GameConstants.puzzleRotation)
        {
            buttonsGroup.SetActive(false);
            //background.SetActive(false);

            // Activate RotatePuzzle
            puzzles[0].SetActive(true);
        }

        if (actionName == GameConstants.puzzleGuessNum)
        {
            buttonsGroup.SetActive(false);
            background.SetActive(false);

            // Activate Guess Puzzle
            puzzles[1].SetActive(true);

        }

        if (actionName == GameConstants.puzzleLock)
        {
            buttonsGroup.SetActive(false);
            background.SetActive(false);

            //Activate Lock
            puzzles[2].SetActive(true);
        }
        */
        if (actionName == GameConstants.actionNextRoom)
        {
            if (completedPuzzles >= TotalPuzzles) SceneManager.LoadScene(currentLevel + 1);
            else Debug.Log("You have not completed all puzzles");            
        }
    }
}
