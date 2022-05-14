using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabSettings : MonoBehaviour
{
    public bool PrefabOnCanvas;

    private GameObject puzzleButton;

    public LevelController GetLevelController()
    {
        return GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    public GameObject GetButton()
    {
        return puzzleButton;
    }

    public void SetButton(GameObject button)
    {
        puzzleButton = button;
    }
}
