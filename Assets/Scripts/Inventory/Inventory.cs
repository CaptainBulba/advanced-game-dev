using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryObject;
    public GameObject canvasObject;
    public GameObject toggleButton;

    public bool[] isFull;
    public GameObject[] slots;

    private bool displayInventory = true;

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

    public void ToggleInv()
    {
        if (displayInventory)
        {
            inventoryObject.SetActive(false);
            toggleButton.transform.SetParent(canvasObject.transform);
            displayInventory = false;

        }
        else
        {
            inventoryObject.SetActive(true);
            toggleButton.transform.SetParent(inventoryObject.transform);
            displayInventory = true;
        }
    }

    public void ToggleInvFully(bool option)
    {
        inventoryObject.SetActive(option);
        toggleButton.SetActive(option);
    }

    public void DeleteItem(int slotNumber)
    {
        Destroy(slots[slotNumber].transform.GetChild(0));
        isFull[slotNumber] = false;
    }
}
