using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    private List<GameObject> inventory = new List<GameObject>();
    private int invSize = 9;
    
    public void Take(GameObject item)
    {
        if(inventory.Count != invSize)
        {
            inventory.Add(item);
            Debug.Log(item.name + " has been added to the inventory");
            Destroy(item.gameObject);
            
        }else
        {
            //The inventory is full, so we can't add the item
            Debug.Log("Inventory is full");
        }
   
    }

}
