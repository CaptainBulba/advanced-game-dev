using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseItem : MonoBehaviour
{
   public void Close()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
