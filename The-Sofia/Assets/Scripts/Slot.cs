using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon; // Reference to the icon game object for this slot
    public GameObject slotParent;
    public bool isFull = false; // Whether this slot is full or not

    private Inventory inventory; // Reference to the inventory script
    private int slotIndex = -1; // The index of this slot in the inventory list

    void Start()
    {
        inventory = transform.parent.GetComponentInParent<Inventory>(); // Get a reference to the inventory script
        icon = transform.Find("Icon").GetComponent<Image>();
        slotParent = GameObject.Find("SlotParent");
    }

    void Update()
    {
        if (slotParent.activeSelf == true)
        {
            UpdateSlot();
        }
    }

    void UpdateSlot()
    {
        // Check if there are any items in the inventory
        if (inventory.items.Count == 0)
        {
            return;
        }

        // Find the first empty slot and mark it as full
        for (int i = 0; i < inventory.inventorySize; i++)
        {
            if (slotParent.transform.GetChild(i).GetComponent<Slot>().isFull == false)
            {
                slotIndex = i;
                slotParent.transform.GetChild(slotIndex).GetComponent<Slot>().isFull = true;
                break;
            }
        }

        // If there is no empty slot, return
        if (slotIndex == -1)
        {
            return;
        }

        // Check if the current slot is full and has a valid item index
        if (isFull == true && slotIndex >= 0 && slotIndex < inventory.items.Count)
        {
            icon.enabled = true;
            icon.sprite = inventory.items[slotIndex].GetComponent<Weapon>().itemSprite;
        }
    }
}