using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject background;

    public GameObject canvas;

    //Will contain the puzzle prefabs
    [HideInInspector]
    public GameObject[] puzzles;

    public GameObject buttonsGroup;
    
    //Will contain an array of all the buttons within the level
    public Button[] puzzleButtons;

    public int totalLevelPuzzles;

    protected int completedPuzzles = 0;
    protected int currentLevel;

    private Inventory inventory;

    void OnEnable()
    {
        inventory = GetComponent<Inventory>();
    }

    public void LaunchMainScreen()
    {
        Debug.Log("Returned to main");

        buttonsGroup.SetActive(true);
        background.SetActive(true);

        for (int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].SetActive(false);
        }

        inventory.ToggleInventory(true);
    }

    public void LaunchMainScreen(GameObject buttonToDelete)
    {
        Debug.Log("Finished the puzzle and returned to main");
        completedPuzzles++;

        buttonsGroup.SetActive(true);
        background.SetActive(true);

        for (int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].SetActive(false);
        }

        inventory.ToggleInventory(true);

        Destroy(buttonToDelete);
    }

    public void LaunchPuzzle(int actionIndex)
    {
        buttonsGroup.SetActive(false);

        inventory.ToggleInventory(false);
        //We can instead also may be change the opacity
        //background.SetActive(false); 

        // Activate puzzle according to its index
        puzzles[actionIndex].SetActive(true);
    }
    public void NextLevelButton(string actionName)
    {
        if (actionName == GameConstants.actionNextRoom)
        {
            Debug.Log("Completed puzzles so far " + completedPuzzles + " out of #" + totalLevelPuzzles + " in this current level");
            if (completedPuzzles >= totalLevelPuzzles)
            {
                SceneManager.LoadScene(currentLevel + 1);
            }
            else
                Debug.Log("You have not completed all puzzles");            
        }
        else 
            Debug.Log("There is something wrong/missing with the button");
    }
}
