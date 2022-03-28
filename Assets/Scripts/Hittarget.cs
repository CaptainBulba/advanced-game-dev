using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittarget : MonoBehaviour
{
    public float healthOfBox = 50f;

    public void HitDamage(float hitAmount)
    {
        healthOfBox -= hitAmount;
        if(healthOfBox <= 0f)
        {
            boxDestroy();
        }
    }

    void boxDestroy() 
    {
        Destroy(gameObject);
    }
}
