using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoController : LevelController
{
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("You are in level " + currentLevel);

        //Get the number of puzzles from the enum
        totalLevelPuzzles = System.Enum.GetNames(typeof(LevelTwoPuzzles)).Length;

        //Except moving to the next level, Currently it is done manual in the unity editor
        puzzles = new GameObject[totalLevelPuzzles];

        for (int i = 0; i < totalLevelPuzzles; i++)
        {
            //Initiating the puzzle LevelTwoPuzzles using the information stored in the LevelTwoPuzzles enum
            var actionName = (LevelTwoPuzzles)i;
            GameObject puzzle = Instantiate(Resources.Load(actionName.ToString()), transform.position, Quaternion.identity, canvas.transform) as GameObject;
            puzzles[i] = puzzle;
        }
    }

}
