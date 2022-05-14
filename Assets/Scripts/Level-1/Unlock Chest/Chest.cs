using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Text[] numbers;
    public int[] correctAnswer;

    private int isCorrect = 0;

    private LevelController levelController;
    
    void Start()
    {
        levelController = GetComponent<PrefabSettings>().GetLevelController();
    }

    public void ChangeNumber(Text chestText)
    {
        int currentNumber = int.Parse(chestText.text) + 1;

        if (currentNumber == 10) currentNumber = 0;

        chestText.text = currentNumber.ToString();
        CheckCondition();
    }

    private void CheckCondition()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            if (int.Parse(numbers[i].text) == correctAnswer[i])
            {
                isCorrect++;
                Debug.Log(isCorrect);
            }
            
        }

        if (isCorrect == 3)
        {
            Debug.Log("You win");
            levelController.LaunchMainScreen();
        }
        else isCorrect = 0;
    }
}
