using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightPuzzle2 : MonoBehaviour
{
    public GameObject centralObject;

    public GameObject[] boxes; // boxes on main object
    public Text[] boxesTexts; // text on the boxes
    private bool[] isBoxFull = new bool[3]; // are boxes full
    private float[] boxesSize = new float[3]; // what is their size in kg

    public float[] rightWeight; // sizes of right side
    public Text rightWeightText; // right side text 

    public GameObject incorrectText; // text to be displayed if puzzle completed unsuccessfully. 
    public GameObject correctText; // text to be displayed if puzzle completed successfully. 
    public float displayResultTime; // time to display correct/incorrect message

    private float currentRotation = -45.0f; // angle of main object 

    private int currentLevel = 0; // current stage of the puzzle

    void Start()
    {
        for (int i = 0; i < isBoxFull.Length; i++)
        {
            isBoxFull[i] = false;
        }
    }

    public void AddItem(GameObject item, float boxSize)
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (isBoxFull[i] == false)
            {
                isBoxFull[i] = true;
                boxesSize[i] = boxSize;

                GameObject instanciatedObject = Instantiate(item, boxes[i].transform, false);

                instanciatedObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentRotation);

                UpdateWeightsRotation();
//                IsWeightCorrect();
                break;
            }
        }
    }


    public void UpdateWeightsRotation()
    {
        float leftWeight = TotalLeftWeight();
        float totalWeight = leftWeight + rightWeight[currentLevel];

        currentRotation = (leftWeight - rightWeight[currentLevel]) / totalWeight * 45;

        centralObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentRotation);

        UpdateLeftTexts();
    }

    private float TotalLeftWeight()
    {
        float leftWeight = 0;
        for (int i = 0; i < boxesSize.Length; i++)
        {
            leftWeight += boxesSize[i];
        }
        return leftWeight;
    }

    private void UpdateLeftTexts()
    {
        for (int i = 0; i < boxesSize.Length; i++)
        {
            boxesTexts[i].text = boxesSize[i] + " kg";
        }
    }

    private void UpdateRightText()
    {
        rightWeightText.text = rightWeight[currentLevel] + " kg";
    }

    private void CleanAllBoxes()
    {
        incorrectText.SetActive(false);

        for (int i = 0; i < boxes.Length; i++)
        {
            Destroy(boxes[i].transform.GetChild(0));
            boxesSize[i] = 0;
            isBoxFull[i] = false;
        }

        UpdateLeftTexts();
        UpdateRightText();
        UpdateWeightsRotation();
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private void IsWeightCorrect()
    {
        if(IsBoxesFull())
        {
            if (Mathf.Approximately(TotalLeftWeight(), rightWeight[GetCurrentLevel()]))
                StartCoroutine(DisplayResult(true));
            else
                StartCoroutine(DisplayResult(false));
        }
    }

    public bool IsBoxesFull()
    {
        int fullBoxesNum = 0;

        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].transform.GetChild(0) != null) fullBoxesNum++;
        }

        if (fullBoxesNum == (boxes.Length + 1)) 
            return true;
        else 
            return false;
    }

    IEnumerator DisplayResult(bool value)
    {
        if(value) correctText.SetActive(true);
        if (!value) incorrectText.SetActive(true);


        yield return new WaitForSeconds(displayResultTime);

        currentLevel++;
        CleanAllBoxes();
    }
}