using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public static int counter;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
            counter = counter + 1;
    }
}
