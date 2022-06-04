using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnItem : MonoBehaviour
{
    public GameObject displayObject;

    public void UseItem()
    {
        Instantiate(displayObject, gameObject.transform.parent.parent, false);
    }
}

