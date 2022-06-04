using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePuzzle : MonoBehaviour
{
    public PrefabSettings prefabSettings;
    private LevelController levelController;

    void Start()
    {
        levelController = prefabSettings.GetLevelController();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            levelController.LaunchMainScreen();
        }
    }
}
