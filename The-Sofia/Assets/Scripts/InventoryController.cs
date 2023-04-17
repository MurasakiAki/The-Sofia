using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private GameObject inventoryCanvas;
    private List<GameObject> inventory = new List<GameObject>();
    private int invSize = 9;

    private List<GameObject> weaponSlot = new List<GameObject>();
    private List<GameObject> armorSlot = new List<GameObject>();
    
    void Start()
    {
        inventoryCanvas = GameObject.Find("Inventory");
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(true);
        }
        else
        {
            inventoryCanvas.SetActive(false);
        }
    }

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

    public void Equip()
    {

    }

}
