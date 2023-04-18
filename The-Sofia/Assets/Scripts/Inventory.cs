using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventorySize = 9; // Set the size of the inventory here
    public List<GameObject> items = new List<GameObject>();

    //Add item
    public bool AddItem(GameObject itemToAdd)
    {
        if (items.Count < inventorySize)
        {
            items.Add(itemToAdd);
            return true;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }
    
    //Remove by index
    public bool RemoveItem(GameObject itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            return true;
        }
        else
        {
            Debug.Log("Item not found in inventory!");
            return false;
        }
    }
}