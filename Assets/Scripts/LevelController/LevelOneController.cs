using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneController : LevelController
{
    // Start is called before the first frame update
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
        }
    }

}
