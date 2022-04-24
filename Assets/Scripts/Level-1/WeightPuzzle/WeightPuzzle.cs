using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightPuzzle : MonoBehaviour
{
    public GameObject weightsObject; // main object (black rectangular)

    public GameObject[] boxes; // boxes on main object

    public bool[] isBoxFull; // are boxes full

    private float[] boxesSize = new float[3]; // what is their size in kg

    public Text[] boxesTexts; // text on the boxes

    private GameObject[] boxesPrefabs = new GameObject[3];

    public float[] rightWeight; // sizes of right side

    public Text rightWeightText; // right side text 

    public GameObject incorrectText; // text to be displayed if puzzle completed unsuccessfully. 

    public float displayIncorrectTime;

    private int currentLevel = 0; // current stage of the puzzle

    private float currentRotation = -45.0f; // angle of main object 

    void Start()
    {
        UpdateWeightsRotation();
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
                
                GameObject instanciatedObject = Instantiate(item, boxes[i].transform, false);
                boxesPrefabs[i] = instanciatedObject;

                instanciatedObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentRotation);

                UpdateWeightsRotation();
                IsWeightCorrect();
                break;
            }
        }
    }

    public void UpdateWeightsRotation()
    {
        float leftWeight = TotalLeftWeight();
        float totalWeight = leftWeight + rightWeight[GetCurrentLevel()];

        currentRotation = (leftWeight - rightWeight[GetCurrentLevel()]) / totalWeight * 45;

        weightsObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentRotation);

        UpdateLeftTexts();
    }

    private void IsWeightCorrect()
    {
        if (Mathf.Approximately(TotalLeftWeight(), rightWeight[GetCurrentLevel()]))
            Debug.Log("You win");
        else if(isBoxesPrefabsNull())
            StartCoroutine(IncorrectAnswer());
    }

   

    IEnumerator IncorrectAnswer()
    {
        incorrectText.SetActive(true);
        
        yield return new WaitForSeconds(displayIncorrectTime);

        incorrectText.SetActive(false);

        for (int i = 0; i < boxesPrefabs.Length; i++)
        {
            Destroy(boxesPrefabs[i]);
            boxesPrefabs[i] = null;
            boxesSize[i] = 0;
            isBoxFull[i] = false;
        }

        UpdateLeftTexts();
        UpdateWeightsRotation();
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

    private bool isBoxesPrefabsNull()
    {
        for (int i = 0; i < boxesPrefabs.Length; i++)
        {
            if (boxesPrefabs[i] == null) return false;
        }
        return true;
    }

    private void UpdateRightText()
    {
        rightWeightText.text = rightWeight[GetCurrentLevel()] + " kg";
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
