using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AdditionRow
{
    public int[] values;
    public int GetTotal()
    {
        int total = 0;
        foreach (int value in values)
        {
            total += value;
        }
        return total;
    }
}

public class GuessNumber : MonoBehaviour
{
    public GameObject[] numberText;
    public Button[] answerBoxes;

    [SerializeField] private AdditionRow[] additionRows = new AdditionRow[4];

    private int[][] numbersList = new int[3][] { new int[] { 1, 2, 3, 6, // Level 1: Row 1
        5, 6, 7, 18, // Row 2
        9, 10, 11, 30, // Row 3
        13, 14, 15 }, // Row 4

        new int[] { 1, 1, 1, 3, // Level 2: Row 1 
            2, 2, 2, 6, // Row 2
            3, 3, 3, 9, // Row 3
            4, 4, 4 }, // Row 4

        new int[] { 1, 1, 1, 1, // Level 3: Row 1
            2, 2, 2, 8, // Row 2
            3, 3, 3, 27, // Row 3
            4, 4, 4 }};  // Row 4

    private int[][] answerList = new int[3][] { new int[] { 39, 40, 41, 42 }, // level 1

        new int[] { 12, 16, 22, 8 }, // level 2
        
        new int[] { 72, 64, 52, 44 }}; // level 3

    private int[] correctAnswers = new int[] { 42, 12, 64 }; // Answers for level 1, 2, 3

    private int maxLevel = 3;
    private int currentLevel = 0;

    void Start()
    {
        LoadTableText();
        LoadAnswerText();
    }

    private void LoadTableText()
    {
        for (int i = 0; i < numbersList[currentLevel].Length; i++)
        {
            numberText[i].GetComponent<Text>().text = numbersList[currentLevel][i].ToString();
        }
    }

    private void LoadAnswerText()
    {
        for (int i = 0; i < answerList[currentLevel].Length; i++)
        {
            answerBoxes[i].GetComponentInChildren<Text>().text = answerList[currentLevel][i].ToString();
        }
    }

    public void ButtonClick(Button clickedButton)
    {   
        if (currentLevel >= maxLevel) return; // temporary solution before we create scenes

        if (clickedButton.GetComponentInChildren<Text>().text == correctAnswers[currentLevel].ToString())
        {
            Debug.Log("Correct answer");

            currentLevel++;

            if (currentLevel < maxLevel)
            {
                LoadTableText();
                LoadAnswerText();
            }
            else Debug.Log("Game Over");
        }
        else Debug.Log("Incorrect Answer");
    }
}
