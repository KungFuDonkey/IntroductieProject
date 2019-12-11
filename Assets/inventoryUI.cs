using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject jinventoryUI;

    inventory jinventory;

    InventorySlot[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        jinventory = inventory.instance;
        jinventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        jinventoryUI.SetActive(!jinventoryUI.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        { 
            jinventoryUI.SetActive(!jinventoryUI.activeSelf);

            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void UpdateUI()
    {
        for (int i=0; i<slots.Length; i++)
        {
            if (i<jinventory.items.Count)
            {
                slots[i].AddItem(jinventory.items[i]);
            } 
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
