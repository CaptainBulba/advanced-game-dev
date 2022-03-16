using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessNumber : MonoBehaviour
{
    public GameObject[] numberText;
    public Button[] answerBoxes;

    [SerializeField] private NumbersRow[] firstRound = new NumbersRow[4];
    [SerializeField] private NumbersRow[] secondRound = new NumbersRow[4];
    [SerializeField] private NumbersRow[] thirdRound = new NumbersRow[4];

    private NumbersRow[] numbersRows;

    private LevelController levelController;

    private int[] correctAnswers = new int[] { 42, 12, 64 }; // Answers for level 1, 2, 3

    private int maxLevel = 3;
    private int currentRound = 0;
    private int tableRowsAmount = 4;
    private int answerRow = 4;

    void Start()
    {
        levelController = GetComponent<LevelController>();
        LoadTableText();
        LoadAnswerText();
    }

    private void LoadTableText()
    {
        GetCurrentNumberRow();
        int numberToDisplay = 0;
        for (int j = 0; j < tableRowsAmount; j++)
        {
            for (int i = 0; i < numbersRows[j].values.Length; i++)
            {
                numberText[numberToDisplay].GetComponentInChildren<Text>().text = numbersRows[j].values[i].ToString();
                numberToDisplay++;
            }
        }
    }

    private void LoadAnswerText()
    {
        GetCurrentNumberRow();
        for (int i = 0; i < numbersRows[answerRow].values.Length; i++)
        {
            answerBoxes[i].GetComponentInChildren<Text>().text = numbersRows[answerRow].values[i].ToString();
        }
    }

    private void GetCurrentNumberRow()
    {
        switch(currentRound)
        {
            case 0:
                numbersRows = firstRound;
                break;

            case 1:
                numbersRows = secondRound;
                break;

            case 2:
                numbersRows = thirdRound;
                break;
        }
    }
    
    public void ButtonClick(Button clickedButton)
    {   
        if (clickedButton.GetComponentInChildren<Text>().text == correctAnswers[currentRound].ToString())
        {
            Debug.Log("Correct answer");

            currentRound++;

            if (currentRound < maxLevel)
            {
                LoadTableText();
                LoadAnswerText();
            }
            else levelController.LaunchMainScreen();
        }
        else Debug.Log("Incorrect Answer");
    } 
}
