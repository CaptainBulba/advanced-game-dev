using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject background;

    public GameObject canvas;

    public GameObject character;

    //Will contain the puzzle prefabs
    [HideInInspector]
    public GameObject[] puzzles;

    public GameObject buttonsGroup;
    
    //Will contain an array of all the buttons within the level
    public Button[] puzzleButtons;

    public int totalLevelPuzzles;

    protected int completedPuzzles = 0;
    protected int currentLevel;

    public float startPuzzleTime;

    private Inventory inventory;
    private PlayerMovement playerMovement;

    [HideInInspector]
    public PlayerText playerText;

    public AudioClip levelMusic;

    void OnEnable()
    {
        inventory = GetComponent<Inventory>();
        playerMovement = character.GetComponent<PlayerMovement>();
        playerText = character.GetComponent<PlayerText>();
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

        character.SetActive(true);
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

        character.SetActive(true);
    }

    public void LaunchPuzzle(GameObject buttonObject)
    {
        ButtonSettings buttonSettings = buttonObject.GetComponent<ButtonSettings>();

        buttonsGroup.SetActive(false);

        inventory.ToggleInventory(false);

        //We can instead also may be change the opacity
        //background.SetActive(false); 

        playerMovement.MovePlayer(buttonObject);

        if (buttonSettings.puzzleText != null)
            StartCoroutine(playerText.PlayText(buttonSettings.puzzleText));  

        StartCoroutine(DisplayPuzzle(buttonObject.GetComponent<ButtonSettings>().actionIndex));
    }

    IEnumerator DisplayPuzzle(int actionIndex)
    {
        yield return new WaitForSeconds(startPuzzleTime);

        character.SetActive(false);

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
            {
                StartCoroutine(playerText.PlayText("I have not completed all challanges in this room yet."));
                //Debug.Log("You have not completed all puzzles");
            }
        }
        else 
            Debug.Log("There is something wrong/missing with the button");
    }
}
