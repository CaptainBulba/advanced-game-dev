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

    public GameObject inventoryItem;
    
    private GameObject puzzleButton;

    private int[] correctAnswers = new int[] { 42, 12, 64 }; // Answers for level 1, 2, 3

    private int maxLevel = 3;
    private int currentRound = 0;
    private int tableRowsAmount = 4;

    void Start()
    {
        levelController = GetComponent<PrefabSettings>().GetLevelController();
        puzzleButton = GetComponent<PrefabSettings>().GetButton();
        LoadTableText();
        LoadAnswerText();
    }

    private void LoadTableText()
    {
        GetCurrentNumberRow();
        for (int j = 0; j < tableRowsAmount; j++) // 4
        {
            for (int i = 0; i < numbersRows[j].values.Length; i++) // 4
            {
                numberText[tableRowsAmount * j + i].GetComponentInChildren<Text>().text = numbersRows[j].values[i].ToString();
            }
        }
    }

    private void LoadAnswerText()
    {
        GetCurrentNumberRow();
        for (int i = 0; i < numbersRows[numbersRows.Length - 1].values.Length; i++)
        {
            answerBoxes[i].GetComponentInChildren<Text>().text = numbersRows[numbersRows.Length - 1].values[i].ToString();
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
            currentRound++;

            if (currentRound < maxLevel)
            {
                LoadTableText();
                LoadAnswerText();
            }
            else
            {
                levelController.LaunchMainScreen(puzzleButton);
                Inventory.Instance.AddItem(inventoryItem);
            }
        }
    } 
}
