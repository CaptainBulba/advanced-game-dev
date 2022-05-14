using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string originalName;
    void Start()
    {
        Debug.Log(gameObject.name);
        originalName = gameObject.name;
    }

    public string GetItemName()
    {
        return originalName;
    }
}
