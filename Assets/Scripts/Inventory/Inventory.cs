using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public void AddItem(GameObject inventoryItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false)
            {
                isFull[i] = true;
                Instantiate(inventoryItem, slots[i].transform, false);
                break;
            }
        }
    }

    public void DeleteItem(int slotNumber)
    {
        Destroy(slots[slotNumber].transform.GetChild(0));
        isFull[slotNumber] = false;
    }
}
