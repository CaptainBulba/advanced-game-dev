using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionClick : MonoBehaviour
{
    public WeightPuzzle weightPuzzle; // Controller script 

    public Text objectText; 

    public float[] objectWeight; // weight size for different levels

    public GameObject[] objectPrefabs; // prefabs to be displayed in the box

    private int currentLevel;

    void Start()
    {
        currentLevel = weightPuzzle.GetCurrentLevel();
        objectText.text = objectWeight[currentLevel] + " kg";    
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weightPuzzle.AddItem(objectPrefabs[currentLevel], objectWeight[currentLevel]);
        }
    }
}
