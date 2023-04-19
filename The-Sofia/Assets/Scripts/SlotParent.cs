using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotParent : MonoBehaviour
{
      private Inventory inventory; // Reference to the inventory script

    void Start()
    {
        inventory = transform.parent.GetComponentInParent<Inventory>(); // Get a reference to the inventory script
    }

    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            UpdateSlot();
        }
    }

    void UpdateSlot()
    {
        // Check if there are any items in the inventory
        if (inventory.items.Count != 0)
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                //Solve for another item types
                transform.GetChild(i).Find("Icon").GetComponent<Image>().sprite = inventory.items[i].GetComponent<Weapon>().itemSprite;

                int slotIndex = GameLogic.GetNameNumber(transform.GetChild(i).gameObject.name);

                Button destroyButton = transform.GetChild(i).Find("Destroy").GetComponent<Button>();
                destroyButton.interactable = true;
                destroyButton.onClick.RemoveAllListeners();
                destroyButton.onClick.AddListener(() => OnDestroyButtonClick(slotIndex));

                Button iconButton = transform.GetChild(i).GetComponent<Button>();
                iconButton.onClick.RemoveAllListeners();
                iconButton.onClick.AddListener(() => OnIconButtonClick(slotIndex));

            }
        }else
        {
            
        }

        
    }

    // Method called when the destroy button is clicked
void OnDestroyButtonClick(int slotIndex)
{
    if (slotIndex >= inventory.items.Count)
    {
        // Return if the slot is out of range
        return;
    }

    // Remove the item from the inventory
    inventory.items.RemoveAt(slotIndex);

    // Update the slot images and destroy button interactability for all subsequent slots
    for (int i = slotIndex; i < transform.childCount - 1; i++)
    {
        Transform currentSlot = transform.GetChild(i);
        Transform nextSlot = transform.GetChild(i + 1);

        // Set the current slot's image to the next slot's image (if it exists)
        if (i < inventory.items.Count)
        {
            currentSlot.Find("Icon").GetComponent<Image>().sprite = inventory.items[i].GetComponent<Weapon>().itemSprite;
        }
        else
        {
            currentSlot.Find("Icon").GetComponent<Image>().sprite = null;
            currentSlot.Find("Destroy").GetComponent<Button>().interactable = false;
        }

        // Update the destroy button interactability
        nextSlot.Find("Destroy").GetComponent<Button>().interactable = (i + 1 < inventory.items.Count);
    }

    // Update the last slot's image and destroy button interactability if the inventory is full
    if (inventory.items.Count == inventory.inventorySize - 1)
    {
        Transform lastSlot = transform.GetChild(inventory.items.Count);
        lastSlot.Find("Icon").GetComponent<Image>().sprite = null;
        lastSlot.Find("Destroy").GetComponent<Button>().interactable = false;
    }
}


    void OnIconButtonClick(int slotIndex)
    {
        // If the slot is empty, return

        Debug.Log("Item clicked");
        
    }
}