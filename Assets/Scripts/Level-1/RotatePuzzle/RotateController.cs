using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public Transform[] puzzles;

    private LevelController levelController;
    private GameObject puzzleButton;

    public GameObject inventoryItem;

    void Start()
    {
        levelController = GetComponent<PrefabSettings>().GetLevelController();
        puzzleButton = GetComponent<PrefabSettings>().GetButton();

        ShufflePuzzle();
    }

    void ShufflePuzzle()
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            //skip this tile as it is empty
            if (i == 12)
                continue;

            int n = Random.Range(0, 4);
            switch (n)
            {
                case 0:
                    puzzles[i].Rotate(0, 0, 0);
                    break;
                case 1:
                    puzzles[i].Rotate(0, 0, 90);
                    break;
                case 2:
                    puzzles[i].Rotate(0, 0, 180);
                    break;
                case 3:
                    puzzles[i].Rotate(0, 0, 270);
                    break;
            }
        }
    }
    public void CheckPuzzlePos()
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            //After rotation angle doesn't get exactly to zero instead to a very small value e.g. 9.659347E-06
            if (Mathf.Floor(puzzles[i].rotation.eulerAngles.z) != 0)
            {
                //Will return without doing anything if at least one element is not in correct rotation
                return;
            }
        }

        //Exit the puzzle and return to level
        levelController.LaunchMainScreen(puzzleButton);
        Inventory.Instance.AddItem(inventoryItem);
    }
}
