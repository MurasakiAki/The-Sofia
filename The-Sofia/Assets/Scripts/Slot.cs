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
    private Button destroyButton; // Reference to the destroy button component

    void Start()
    {
        inventory = transform.parent.GetComponentInParent<Inventory>(); // Get a reference to the inventory script
        icon = transform.Find("Icon").GetComponent<Image>();
        slotParent = GameObject.Find("SlotParent");

        // Get a reference to the destroy button component
        destroyButton = transform.Find("Destroy").GetComponent<Button>();

        // Add a click listener to the destroy button
        destroyButton.onClick.AddListener(OnDestroyButtonClick);

        // Set the interactable property of the destroy button based on the value of isFull
        destroyButton.interactable = isFull;

        // Add a button component to the icon game object
        Button iconButton = icon.gameObject.AddComponent<Button>();

        // Add a click listener to the icon button
        iconButton.onClick.AddListener(OnIconButtonClick);
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

        // If there is no empty slot, set slotIndex to -1
        if (slotIndex == -1)
        {
            return;
        }

        // Check if the current slot is full and has a valid item index
        if (isFull == true && slotIndex >= 0 && slotIndex < inventory.items.Count)
        {
            icon.enabled = true;
            icon.sprite = inventory.items[slotIndex].GetComponent<Weapon>().itemSprite;

            // Set the interactable property of the destroy button to true
            destroyButton.interactable = true;
        }
        else
        {
            // Set the icon to null when the slot is not full
            icon.enabled = false;
            icon.sprite = null;

            // Set the interactable property of the destroy button to false
            destroyButton.interactable = false;
        }
    }

    // Method called when the destroy button is clicked
    void OnDestroyButtonClick()
    {
        // Set the current slot to empty
        isFull = false;
    
        // Remove the item from the inventory list and update the indices of the slots
        inventory.items.Remove(icon.gameObject);
        for (int i = 0; i < slotParent.transform.childCount; i++)
        {
            Slot slot = slotParent.transform.GetChild(i).GetComponent<Slot>();
            if (slot.isFull && slot.slotIndex > slotIndex)
            {
                slot.slotIndex--;
            }
        }
    
        // Set the icon to null
        icon.enabled = false;
        icon.sprite = null;

        // Set the interactable property of the destroy button to false
        destroyButton.interactable = false;
    }

    void OnIconButtonClick()
    {
        // If the slot is empty, return
        if (!isFull)
        {
            return;
        }else
        {
            Debug.Log("Item clicked");
        }
    }
}