using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleController : MonoBehaviour
{
    public BottleController firstBottle;
    public BottleController secondBottle;

    const int countWinCondition = 6;
    bool currentlyTransfering = false;
    private LevelController levelController;

    [SerializeField]
    int countCompleteBottles = 0;

    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelTwoController>();
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

                                Debug.Log("After color transfer:");
                                Debug.Log("Number of top colors in first bottle is " + firstBottle.numberOfTopColorLayers);
                                Debug.Log("Number of top colors in second bottle is " + secondBottle.numberOfTopColorLayers);
    
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
            StartCoroutine(WaitBeforeClosing());
            levelController.LaunchMainScreen();
        }
            
    }
    IEnumerator WaitBeforeClosing()
    {
        yield return new WaitForSeconds(20.0f);

    }
}

