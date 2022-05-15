using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
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
        else closeButton.transform.SetParent(gameObject.transform);

        invVisability = !invVisability;
        gameObject.SetActive(invVisability);
    }

    public void ToggleInventory(bool value)
    {
        invVisability = value;
        gameObject.SetActive(value);
    }

    public void DeleteItem(GameObject prefabName)
    {
        Debug.Log(Equals(slots[0].transform.GetChild(0).gameObject, prefabName));
        Debug.Log(slots[0].transform.GetChild(0).gameObject == prefabName);
        Debug.Log(slots[0].transform.GetChild(0).gameObject.name);

        for (int i = 0; i < slots.Length; i++)
        {
            

            if (slots[i].transform.childCount > 0 && ReferenceEquals(slots[i].transform.GetChild(0).gameObject, prefabName))
                Destroy(slots[i].transform.GetChild(0).gameObject);
        }
    }
}