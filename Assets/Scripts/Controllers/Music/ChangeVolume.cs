using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MusicController.Instance.ChangeVolume();
        }
    }
}
