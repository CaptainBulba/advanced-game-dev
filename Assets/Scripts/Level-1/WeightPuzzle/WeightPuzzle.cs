using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightPuzzle : MonoBehaviour
{
    public GameObject centralObject;

    public GameObject[] boxes; // boxes on main object
    public Text[] boxesTexts; // text on the boxes
    private bool[] isBoxFull = new bool[3]; // are boxes full
    private float[] boxesSize = new float[3]; // what is their size in kg
    public GameObject[] options; // bottom options

    public float[] rightWeight; // sizes of right side
    public Text rightWeightText; // right side text 

    public GameObject incorrectText; // text to be displayed if puzzle completed unsuccessfully. 
    public GameObject correctText; // text to be displayed if puzzle completed successfully. 
    public float displayResultTime; // time to display correct/incorrect message

    private float currentRotation = 0; // angle of main object 

    private int currentLevel = 0; // current stage of the puzzle

    private LevelController levelController;
    private GameObject puzzleButton;
    private Inventory inventory;
    public GameObject inventoryItem;


    void OnEnable()
    {
        CleanAllBoxes();

        levelController = GetComponent<PrefabSettings>().GetLevelController();
        puzzleButton = GetComponent<PrefabSettings>().GetButton();
        inventory = GetComponent<PrefabSettings>().GetInventory();

        currentLevel = 0;

        correctText.SetActive(false);
        incorrectText.SetActive(false);

        for (int i = 0; i < isBoxFull.Length; i++)
        {
            isBoxFull[i] = false;
        }

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<OptionClick>().UpdateText();
        }

        UpdateRightText();
    }

    public void AddItem(GameObject item, float boxSize)
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (isBoxFull[i] == false)
            {
                isBoxFull[i] = true;
                boxesSize[i] = boxSize;

                UpdateWeightsRotation();

                GameObject instanciatedObject = Instantiate(item, boxes[i].transform, false);

                instanciatedObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentRotation);

                IsWeightCorrect();
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
        correctText.SetActive(false);

        for (int i = 0; i < boxes.Length; i++)
        {
            if(boxes[i].transform.childCount > 0)
                Destroy(boxes[i].transform.GetChild(0).gameObject);
            boxesSize[i] = 0;
            isBoxFull[i] = false;
        }

        UpdateLeftTexts();
        UpdateRightText();
        UpdateWeightsRotation();

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<OptionClick>().UpdateText();
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private void IsWeightCorrect()
    {
        if (IsBoxesFull())
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
            if (boxes[i].transform.childCount > 0) fullBoxesNum++;
        }

        if (fullBoxesNum == boxes.Length)
            return true;
        else
            return false;
    }

    IEnumerator DisplayResult(bool value)
    {
        if (value)
        {
            correctText.SetActive(true);
            currentLevel++;
        } 
        else 
            incorrectText.SetActive(true);

        yield return new WaitForSeconds(displayResultTime);

        if (currentLevel == rightWeight.Length)
        {
            inventory.AddItem(inventoryItem);
            levelController.LaunchMainScreen(puzzleButton);
        }
        else
            CleanAllBoxes();
    }
}

