using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleController : MonoBehaviour
{
    public BottleController firstBottle;
    public BottleController secondBottle;
    public BottleController[] bottles;

    const int countWinCondition = 6;
    bool currentlyTransfering = false;
    [SerializeField]
    int countCompleteBottles = 0;

    private LevelController levelController;
    private GameObject puzzleButton;

    private Inventory inventory;
    public GameObject inventoryItem;
    public GameObject restart;


    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelTwoController>();
        puzzleButton = GetComponent<PrefabSettings>().GetButton();
        inventory = GetComponent<PrefabSettings>().GetInventory();
        
    }

    private void OnEnable()
    {
        BottleController.OnBottleComplete += UpdateBottleComplete;
        BottleController.OnFinishColorTransfer += FinishColorTransfer;
    }

    private void OnDisable()
    {
        BottleController.OnBottleComplete -= UpdateBottleComplete;
        BottleController.OnFinishColorTransfer -= FinishColorTransfer;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !currentlyTransfering)
        {
            //check on what we are clicking
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "Restart")
                {
                    Debug.Log("Restart section");
                    RestartPuzzle();
                }

                //check if we hit a bottle
                if(hit.collider.GetComponent<BottleController>() != null)
                {
                    //No bottles have been clicked yet
                    if(firstBottle == null)
                    {
                        //set the first bottle clicked
                        firstBottle = hit.collider.GetComponent<BottleController>();

                        //check if bottle is not empty
                        if (firstBottle.IsBottleEmpty())
                        {
                            //Don't allow selecting an empty bottle as a first bottle
                            firstBottle = null;
                        }
                    }
                    else
                    {
                        //Basically if we click on the first bottle twice it will unclick
                        if(firstBottle == hit.collider.GetComponent<BottleController>())
                        {
                            firstBottle = null;
                        }
                        else
                        {
                            //set the second clicked item to the second bottle and add a reference to it in the first bottle
                            secondBottle = hit.collider.GetComponent<BottleController>();
                            firstBottle.bottleControllerRef = secondBottle;

                            //Update the top colors for both bottles
                            firstBottle.UpdateTopColorValues();
                            secondBottle.UpdateTopColorValues();

                            //check if we can fill second bottle with first
                            if(secondBottle.FillBottleCheck(firstBottle.topColor))
                            {
                                currentlyTransfering = true;
                                firstBottle.StartColorTransfer();
                            }

                            //After finishing the color transfer Reset the selections
                            firstBottle = null;
                            secondBottle = null;

                        }
                    }
                }
            }
        }
    }
    
    void RestartPuzzle()
    {
        Debug.Log("Restarting puzzle ... ");
        //Reset the colors in all bottles
        foreach (BottleController bottle in bottles)
        {
            bottle.ResetBottleColors();
        }

        //Reset bottle selects
        firstBottle = null;
        secondBottle = null;

        countCompleteBottles = 0;

    }

    void FinishColorTransfer()
    {
        currentlyTransfering = false;
    }

    void UpdateBottleComplete()
    {
        countCompleteBottles++;
        Debug.Log("Color Puzzle Controller, current completed bottles " + countCompleteBottles);
        if (countCompleteBottles == countWinCondition)
        {
            levelController.LaunchMainScreen(puzzleButton);
        }
            
    }
    IEnumerator WaitBeforeClosing()
    {

        yield return new WaitForEndOfFrame();


    }
}

