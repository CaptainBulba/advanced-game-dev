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

    // Start is called before the first frame update
    void Start()
    {
        UpdateColorsOnShader();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            StartCoroutine(RotateBottle());
        }
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

        while(t < timeToRotate)
        {
            lerpValue = t / timeToRotate;
            angleValue = Mathf.Lerp(0.0f, 90.0f, lerpValue);
              
            transform.eulerAngles = new Vector3(0,0,angleValue);
            bottleMaskSprite.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));
            bottleMaskSprite.material.SetFloat("_FillAmount", FillAmountCurve.Evaluate(angleValue));


            t += Time.deltaTime * RotationSpeedMultiplier.Evaluate(angleValue);

            yield return new WaitForEndOfFrame();
        }

        angleValue = 90.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskSprite.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));
        bottleMaskSprite.material.SetFloat("_FillAmount", FillAmountCurve.Evaluate(angleValue));

        StartCoroutine(RotateBottleBack());
    }

    //lerp between angle values 0 and 90 to rotate the bottle
    IEnumerator RotateBottleBack()
    {
        float t = 0;
        float lerpValue;
        float angleValue;

        while (t < timeToRotate)
        {
            lerpValue = t / timeToRotate;
            angleValue = Mathf.Lerp(90.0f, 0.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            bottleMaskSprite.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));


            t += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        angleValue = 0.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskSprite.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));


    }
}
