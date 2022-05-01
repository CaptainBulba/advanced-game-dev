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


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weightPuzzle.AddItem(objectPrefabs[weightPuzzle.GetCurrentLevel()], objectWeight[weightPuzzle.GetCurrentLevel()]);
        }
    }

    public void UpdateText()
    {
        objectText.text = objectWeight[weightPuzzle.GetCurrentLevel()] + " kg";
    }
}
