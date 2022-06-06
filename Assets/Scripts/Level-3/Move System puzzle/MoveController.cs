using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public int counter;

    private LevelController levelController;
    private GameObject puzzleButton;
    public PrefabSettings prefabSettings;

    public void Start()
    {
        counter = MoveSystem.count;

        puzzleButton = prefabSettings.GetComponent<PrefabSettings>().GetButton();
        levelController = prefabSettings.GetComponent<PrefabSettings>().GetLevelController();

    }
    public void Update()
    {
        counter = MoveSystem.count;
        if (counter == 3)
        {
            levelController.LaunchMainScreen(puzzleButton);
        }
    }
}