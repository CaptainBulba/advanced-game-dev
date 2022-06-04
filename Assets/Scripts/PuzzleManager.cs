using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    #region Singlton

    public static PuzzleManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Button[] puzzleButtons;

    private void Start()
    {
        //Get the number of puzzles from the enum
        int numPuzzles = System.Enum.GetNames(typeof(LevelOnePuzzles)).Length;

         puzzleButtons = new Button[numPuzzles]; 
    }
}
