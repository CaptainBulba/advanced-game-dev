using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject closeButton;

    private bool invVisability = true;


    public void AddItem(GameObject inventoryItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount == 0)
            {
                Instantiate(inventoryItem, slots[i].transform, false);
                break;
            }
        }
    }

    public void ToggleInventory()
    {
        if (invVisability) closeButton.transform.SetParent(gameObject.transform);
        else closeButton.transform.SetParent(closeButton.transform.parent.parent);

        gameObject.SetActive(invVisability);
        invVisability = !invVisability;
    }

    public void DeleteItem(int slotNumber)
    {
        Destroy(slots[slotNumber].transform.GetChild(0));
        isFull[slotNumber] = false;
    }
}
