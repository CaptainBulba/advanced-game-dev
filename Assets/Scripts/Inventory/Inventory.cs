using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
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
        if (invVisability) closeButton.transform.SetParent(closeButton.transform.parent.parent);
        else closeButton.transform.SetParent(inventory.transform);

        invVisability = !invVisability;
        inventory.SetActive(invVisability);
    }

    public void ToggleInventory(bool value)
    {
        invVisability = value;
        inventory.SetActive(value);
    }

    public bool SearchItem(int itemID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0 && slots[i].gameObject.transform.GetChild(0).GetComponent<Item>().itemID == itemID)
                return true;
        }
        return false;
    }

    public void DeleteItem(int itemID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0 && slots[i].gameObject.transform.GetChild(0).GetComponent<Item>().itemID == itemID)
                Destroy(slots[i].transform.GetChild(0).gameObject);
        }
    }
}
