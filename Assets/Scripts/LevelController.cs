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

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void LaunchMainScreen()
    {
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
        if(actionName == "rotate_puzzle")
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
