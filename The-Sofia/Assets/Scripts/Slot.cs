using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public bool isFull;
    public GameObject item;

    private InventoryController controller;

    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<InventoryController>();
    }

    void Update()
    {
        if(isFull)
        {
            gameObject.transform.Find("Icon").GetComponent<Image>().enabled = true;
            Debug.Log(GameLogic.ItemType(item) + " item");
            if(GameLogic.ItemType(item) == "Weapon")
            {
               gameObject.transform.Find("Icon").GetComponent<Image>().sprite = item.GetComponent<Weapon>().itemSprite;
            }
            
        }
        else
        {
            gameObject.transform.Find("Icon").GetComponent<Image>().enabled = false;
        }
    }

    public void Use()
    {
        if (item.GetComponent<Weapon>() != null)
        {
            //Equip weapon
            
        }else
        {
            //Equip armor or use item
        }
    }
}
