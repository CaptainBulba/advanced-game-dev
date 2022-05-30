using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    public Color[] bottleColors;
    public SpriteRenderer bottleMaskSprite;
    
    public float timeToRotate = 1.0f;
    
    //The animation curve stores a collection of Keyframes that can be evaluated over time.
    public AnimationCurve ScaleAndRotationMultiplierCurve; //Values through the Unity editor KeyFrame0: 0.34, KeyFrame30: 0.34, KeyFrame54: 0.13, KeyFrame71: -0.08, KeyFrame83: -0.29, KeyFrame90: -0.5
    public AnimationCurve FillAmountCurve;  //Values through the Unity editor KeyFrame0: 1, KeyFrame90: 0.47
    public AnimationCurve RotationSpeedMultiplier;

    public float[] fillAmounts;
    public float[] rotationValues;

    private int rotationIndex = 0;
    [Range(0,4)]
    public int numberOfColorsInBottle = 4;

    public Color topColor;
    public int numberOfTopColorLayers = 1;

    //Reference to the second bottle to be filled
    public BottleController bottleControllerRef;
    private int numberOfColorsToTransfer = 0;

    //Temporary variable
    public bool justThisBottle = false;

    public Transform leftRotationPoint;
    public Transform rightRotationPoint;
    private Transform selectedRotationPoint;

    private float directionMultiplier = 1.0f;

    Vector3 originalPosition;
    Vector3 startPosition;
    Vector3 endPosition;

    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        bottleMaskSprite.material.SetFloat("_FillAmount", fillAmounts[numberOfColorsInBottle]);

        originalPosition = transform.position;

        UpdateColorsOnShader();
        UpdateTopColorValues();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P)  && justThisBottle == true)
        {
            //Make sure that the top color value is accurate 
            UpdateTopColorValues();
            //Check if the second bottle has space and both colors are matching
            if(bottleControllerRef.FillBottleCheck(topColor))
            {
                //Determine the direction of pouring
                ChooseRotationPointAndDirection();

                numberOfColorsToTransfer = Mathf.Min(numberOfTopColorLayers, 4 - bottleControllerRef.numberOfColorsInBottle);

                //Update the value of the bottle color array
                for(int i = 0; i < numberOfColorsToTransfer; i++)
                {
                    //check the index of the second bottle top based on the current number of colors inside it and add new colors on top of it
                    bottleControllerRef.bottleColors[bottleControllerRef.numberOfColorsInBottle + i] = topColor;
                }
                //Reflect the color changes to screen through the shader graph
                bottleControllerRef.UpdateColorsOnShader();
            }

            CalculateRotationIndex(4 - bottleControllerRef.numberOfColorsInBottle);
            StartCoroutine(RotateBottle());
        }
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

        UpdateTopColorValues();

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
