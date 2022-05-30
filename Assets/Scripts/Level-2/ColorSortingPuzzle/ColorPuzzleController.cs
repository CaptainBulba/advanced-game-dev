using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleController : MonoBehaviour
{
    public BottleController firstBottle;
    public BottleController secondBottle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
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

                            //check if can fill second bottle with first
                            if(secondBottle.FillBottleCheck(firstBottle.topColor))
                            {
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
}
