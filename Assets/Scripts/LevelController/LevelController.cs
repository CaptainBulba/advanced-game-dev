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
    private GameObject[] puzzles;

    public GameObject buttonsGroup;
    
    //Will contain an array of all the buttons within the level
    public Button[] puzzleButtons;

    public int totalLevelPuzzles;

    protected int completedPuzzles = 0;
    protected int currentLevel;

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("You are in level " + currentLevel + 1);

        //Get the number of puzzles from the enum
        totalLevelPuzzles = System.Enum.GetNames(typeof(LevelOnePuzzles)).Length;

        //Except moving to the next level, Currently it is done manual in the unity editor
        puzzles = new GameObject[totalLevelPuzzles];

        for (int i = 0; i < totalLevelPuzzles; i++)
        {
            //Initiating the puzzle prefabs using the information stored in the LevelOneType enum
            var actionName = (LevelOnePuzzles)i;

            GameObject prefabToLoad = Resources.Load(actionName.ToString()) as GameObject;
            bool onCanvas = prefabToLoad.GetComponent<PrefabSettings>().PrefabOnCanvas;

            Transform prefabLocation;
            if (onCanvas) prefabLocation = canvas.transform;
            else prefabLocation = canvas.transform.parent;

            GameObject puzzle = Instantiate(prefabToLoad, transform.position, Quaternion.identity, prefabLocation);

            puzzles[i] = puzzle;

            puzzle.GetComponent<PrefabSettings>().SetButton(buttonsGroup.transform.GetChild(i).gameObject);
        }
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

        Destroy(buttonToDelete);
    }

    public void LaunchPuzzle(int actionIndex)
    {
        buttonsGroup.SetActive(false);
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
