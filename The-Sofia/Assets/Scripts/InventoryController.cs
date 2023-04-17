using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private GameObject inventoryCanvas;
    private GameObject slotParent;
    public List<GameObject> inventory = new List<GameObject>();
    public int invSize = 9;

    public bool hasEquippedWeapon;
    public bool hasEquippedArmor;
    
    void Start()
    {
        inventoryCanvas = GameObject.Find("Inventory");
        slotParent = GameObject.Find("SlotParent");

        if(gameObject.GetComponent<PlayerController>().weapon == 0)
        {
            hasEquippedWeapon = false;
        }else
        {
            hasEquippedWeapon = true;
        }

        if (gameObject.GetComponent<PlayerController>().armor == 0)
        {
            hasEquippedArmor = false;
        }else
        {
            hasEquippedArmor = true;
        }
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
            for(int i = 0; i < invSize; i++)
            {
                if(slotParent.transform.GetChild(i).GetComponent<Slot>().isFull == false)
                {
                    inventory.Add(item);
                    Debug.Log(item.name + " has been added to the inventory");

                    slotParent.transform.GetChild(i).GetComponent<Slot>().item = inventory[i];
                    Debug.Log(slotParent.transform.GetChild(i).name);
                    Debug.Log(GameLogic.ItemType(item));
                    slotParent.transform.GetChild(i).GetComponent<Slot>().isFull = true;
                    break;
                }
                
            }
            //Destroy(item.gameObject);

        }else
        {
            //The inventory is full, so we can't add the item
            Debug.Log("Inventory is full");
        }
   
    }

}
