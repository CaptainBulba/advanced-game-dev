using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSettings : MonoBehaviour
{
    public bool PrefabOnCanvas;

    public LevelController GetLevelController()
    {
        return GameObject.Find("LevelController").GetComponent<LevelController>();
    }
}
