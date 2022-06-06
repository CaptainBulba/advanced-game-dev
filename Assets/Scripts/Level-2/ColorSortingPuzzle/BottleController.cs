using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BottleController : MonoBehaviour
{
    //Bottle color variables
    public Color[] bottleColors;
    public Color topColor;
    public int numberOfTopColorLayers = 1;
    [Range(0, 4)]
    public int numberOfColorsInBottle;

    [SerializeField]
    private Color[] originalBottleColors;
    private int originalNumberOfColorsInBottle;

    public SpriteRenderer bottleMaskSprite;
    
    public float timeToRotate = 1.0f;
    
    //The animation curve stores a collection of Keyframes that can be evaluated over time.
    public AnimationCurve ScaleAndRotationMultiplierCurve; //Values through the Unity editor KeyFrame0: 0.34, KeyFrame30: 0.34, KeyFrame54: 0.13, KeyFrame71: -0.08, KeyFrame83: -0.29, KeyFrame90: -0.5
    public AnimationCurve FillAmountCurve;  //Values through the Unity editor KeyFrame0: 1, KeyFrame90: 0.47
    public AnimationCurve RotationSpeedMultiplier;

    public float[] fillAmounts;
    public float[] rotationValues;

    private int rotationIndex = 0;

    //Reference to the second bottle to be filled
    public BottleController bottleControllerRef;
    private int numberOfColorsToTransfer = 0;

    public Transform leftRotationPoint;
    public Transform rightRotationPoint;
    private Transform selectedRotationPoint;

    private float directionMultiplier = 1.0f;

    Vector3 originalPosition;
    Vector3 startPosition;
    Vector3 endPosition;

    public LineRenderer lineRenderer;
    
    public static UnityAction OnBottleComplete;
    public static UnityAction OnFinishColorTransfer;

    // Start is called before the first frame update
    void Start()
    {
        bottleMaskSprite.material.SetFloat("_FillAmount", fillAmounts[numberOfColorsInBottle]);

        originalPosition = transform.position;

        UpdateColorsOnShader();
        UpdateTopColorValues();

        RecordOriginalValues();
    }

    private void RecordOriginalValues()
    {
        string objName = GetComponent<BottleController>().name.ToString();
        Debug.Log("This is " + objName);
        Debug.Log(" Number of colors " + bottleColors.Length);
        
        //Keep a copy of bottle colors
        for (int i = 0; i < numberOfColorsInBottle; i++)
        {
            originalBottleColors[i] = bottleColors[i];
        }
        originalNumberOfColorsInBottle = numberOfColorsInBottle;

    }    
    public void ResetBottleColors()
    {
        for (int i = 0; i < originalNumberOfColorsInBottle; i++)
        {
            bottleColors[i] = originalBottleColors[i];
            //rotationValues[i] = originalRotationValues[i];
        }

        //numberOfTopColorLayers = 1;
        numberOfColorsInBottle = originalNumberOfColorsInBottle;

        bottleMaskSprite.material.SetFloat("_FillAmount", fillAmounts[numberOfColorsInBottle]);

        transform.position = originalPosition;

        //Reset the colors in game
        UpdateColorsOnShader();
        UpdateTopColorValues();

        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void StartColorTransfer()
    {
        //Determine the direction of pouring
        ChooseRotationPointAndDirection();

        numberOfColorsToTransfer = Mathf.Min(numberOfTopColorLayers, 4 - bottleControllerRef.numberOfColorsInBottle);

        //Update the value of the bottle color array
        for (int i = 0; i < numberOfColorsToTransfer; i++)
        {
            //check the index of the second bottle top based on the current number of colors inside it and add new colors on top of it
            bottleControllerRef.bottleColors[bottleControllerRef.numberOfColorsInBottle + i] = topColor;
        }
        //Reflect the color changes to screen through the shader graph
        bottleControllerRef.UpdateColorsOnShader();

        CalculateRotationIndex(4 - bottleControllerRef.numberOfColorsInBottle);

        //Adjust order in layers to fix overalapping between the bottles during transfer
        transform.GetComponent<SpriteRenderer>().sortingOrder += 2;
        bottleMaskSprite.sortingOrder += 2;

        StartCoroutine(MoveBottle());
    }

    IEnumerator MoveBottle()
    {
        startPosition = transform.position;
        
        if(selectedRotationPoint == leftRotationPoint)
        {
            endPosition = bottleControllerRef.rightRotationPoint.position;
        }
        else
        {
            endPosition = bottleControllerRef.leftRotationPoint.position;
        }

        float t = 0;

        while(t<=1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * 2;

            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        StartCoroutine(RotateBottle());

    }

    IEnumerator MoveBottleBack()
    {
        startPosition = transform.position;
        endPosition = originalPosition;

        float t = 0;

        while (t <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * 2;

            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        //Change the order back to its original value 
        transform.GetComponent<SpriteRenderer>().sortingOrder -= 2;
        bottleMaskSprite.sortingOrder -= 2;

        //Update the color count of the second bottle
        bottleControllerRef.UpdateTopColorValues();

        //Check if the second bottle is full send a trigger and deactivate
        bottleControllerRef.IsBottleComplete();

        Debug.Log("Finished rotation operation thank you!");
        OnFinishColorTransfer?.Invoke();


    }

    //To update colors
    void UpdateColorsOnShader()
    {
        //First parameter Reference name from the shader, Second parameter actual color
        bottleMaskSprite.material.SetColor("_C1", bottleColors[0]);
        bottleMaskSprite.material.SetColor("_C2", bottleColors[1]);
        bottleMaskSprite.material.SetColor("_C3", bottleColors[2]);
        bottleMaskSprite.material.SetColor("_C4", bottleColors[3]);
    }

    //lerp between angle values 0 and 90 to rotate the bottle
    IEnumerator RotateBottle()
    {
        float t = 0;
        float lerpValue;
        float angleValue;

        float lastAngleValue = 0;

        while (t < timeToRotate)
        {
            lerpValue = t / timeToRotate;
            //Adjust the angle to rotate
            angleValue = Mathf.Lerp(0.0f, directionMultiplier * rotationValues[rotationIndex], lerpValue);
              
            //rotate bottle around the points we created
            transform.RotateAround(selectedRotationPoint.position, Vector3.forward, lastAngleValue - angleValue);

            bottleMaskSprite.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

            if (fillAmounts[numberOfColorsInBottle] > FillAmountCurve.Evaluate(angleValue))
            {
                //Activate the line renderer during the filling operation
                if(lineRenderer.enabled == false)
                {
                    //set line render colors
                    lineRenderer.startColor = topColor;
                    lineRenderer.endColor = topColor;

                    //set the positions 
                    lineRenderer.SetPosition(0, selectedRotationPoint.position);
                    lineRenderer.SetPosition(1, selectedRotationPoint.position - Vector3.up * 1.45f);

                    lineRenderer.enabled = true;
                }
                bottleMaskSprite.material.SetFloat("_FillAmount", FillAmountCurve.Evaluate(angleValue));

                //Use the difference between fill amount curve
                bottleControllerRef.FillUp(FillAmountCurve.Evaluate(lastAngleValue) - FillAmountCurve.Evaluate(angleValue));
            }


            t += Time.deltaTime * RotationSpeedMultiplier.Evaluate(angleValue);
            //keep track of the angle value
            lastAngleValue = angleValue;

            yield return new WaitForEndOfFrame();
        }

        angleValue = directionMultiplier * rotationValues[rotationIndex];
        
        bottleMaskSprite.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));
        bottleMaskSprite.material.SetFloat("_FillAmount", FillAmountCurve.Evaluate(angleValue));

        //Update the number of colors after rotation based on the actual transfered number
        numberOfColorsInBottle -= numberOfColorsToTransfer;
        //Update the number of colors for the second bottle
        bottleControllerRef.numberOfColorsInBottle += numberOfColorsToTransfer;

        //disable line renderer
        lineRenderer.enabled = false;
        
        
        StartCoroutine(RotateBottleBack());
    }

    //lerp between angle values 0 and 90 to rotate the bottle
    IEnumerator RotateBottleBack()
    {
        float t = 0;
        float lerpValue;
        float angleValue;
        float lastAngleValue = directionMultiplier * rotationValues[rotationIndex];

        while (t < timeToRotate)
        {
            lerpValue = t / timeToRotate;
            angleValue = Mathf.Lerp(directionMultiplier * rotationValues[rotationIndex], 0.0f, lerpValue);

            transform.RotateAround(selectedRotationPoint.position, Vector3.forward, lastAngleValue - angleValue);
            bottleMaskSprite.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

            lastAngleValue = angleValue;
            t += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        this.UpdateTopColorValues();
        


        //UpdateRotationIndex();

        angleValue = 0.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskSprite.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

        StartCoroutine(MoveBottleBack());

    }

    public void UpdateTopColorValues()
    {
        if(numberOfColorsInBottle !=0)
        {
            numberOfTopColorLayers = 1;

            //update the topColor to match the current level in the bottle
            topColor = bottleColors[numberOfColorsInBottle - 1];

            if(numberOfColorsInBottle == 4)
            {
                if(bottleColors[3].Equals(bottleColors[2]))
                {
                    numberOfTopColorLayers = 2;

                    if (bottleColors[2].Equals(bottleColors[1]))
                    {
                        numberOfTopColorLayers = 3;

                        if (bottleColors[1].Equals(bottleColors[0]))
                        {
                            //All colors inside bottle are matching
                            numberOfTopColorLayers = 4;
                            
                            
                            
                        }
                    }
                }
            }
            else if (numberOfColorsInBottle == 3)
            {
                if (bottleColors[2].Equals(bottleColors[1]))
                {
                    numberOfTopColorLayers = 2;

                    if (bottleColors[1].Equals(bottleColors[0]))
                    {
                        numberOfTopColorLayers = 3;
                    }
                }
            }

            else if (numberOfColorsInBottle == 2)
            {
                if (bottleColors[1].Equals(bottleColors[0]))
                {
                    numberOfTopColorLayers = 2;
                }
            }

            //now when we rotate we need to empty top colors by adjusting the value of rotation index
            rotationIndex = 3 - (numberOfColorsInBottle - numberOfTopColorLayers);
        }
    }


    //check if same colors are on top of each bottle
    //And if second bottle is empty and how much we can add on top of it
    public bool FillBottleCheck(Color colorToCheck)
    {
        //if the bottle is empty return true
        if(numberOfColorsInBottle == 0)
        {
            return true;
        }
        else
        {
            //if the bottle is full return false
            if (numberOfColorsInBottle == 4)
            {
                return false;
            }
            else
            {
                if(topColor.Equals(colorToCheck))
                    return true;
                else
                    return false;
            }
        }
    }

    //Check if the bottle if complete with the same color
    public void IsBottleComplete()
    {
        if (numberOfTopColorLayers == 4)
        {
            //Disable the collider when the bottle is full
            this.GetComponent<BoxCollider2D>().enabled = false;

            //Trigger an event to register to the main puzzle controller
            Debug.Log("Invoking Event from IsBottlecomplete Function");
            OnBottleComplete?.Invoke();

        }

    }

    public bool IsBottleEmpty()
    {
        return numberOfColorsInBottle == 0;
    }

    //This updates the rotation index based on the number of empty spaces the second bottle can accomodate 
    private void CalculateRotationIndex(int numberOfEmptySpacesInBottle)
    {
        //Take the minimum between number of empty spaces in the second bottle and the number of top color layers in the first
        rotationIndex = 3 - (numberOfColorsInBottle - Mathf.Min(numberOfEmptySpacesInBottle, numberOfTopColorLayers));
    }

    private void FillUp(float fillAmountToAdd)
    {
        bottleMaskSprite.material.SetFloat("_FillAmount", bottleMaskSprite.material.GetFloat("_FillAmount") + fillAmountToAdd);

    }
    
    private void ChooseRotationPointAndDirection()
    {
        if(transform.position.x > bottleControllerRef.transform.position.x)
        {
            selectedRotationPoint = leftRotationPoint;
            directionMultiplier = -1.0f;
        }
        else
        {
            selectedRotationPoint = rightRotationPoint;
            directionMultiplier = 1.0f;
        }
    }
}
