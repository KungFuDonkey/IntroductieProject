using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject jinventoryUI;
    public Transform gearParent;

    EquipmentInventory bEquipmnetInventory;
    inventory binventory;
    InventorySlot[] slots;
    EquipmentInventorySlot[] eslots;
    
    void Start()
    {
        binventory = inventory.instance;
        bEquipmnetInventory = EquipmentInventory.instance;

        binventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        eslots = gearParent.GetComponentsInChildren<EquipmentInventorySlot>();
        jinventoryUI.SetActive(false);
    }

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
            if (i<binventory.items.Count)
            {
                slots[i].AddItem(binventory.items[i]);
            } 
            else
            {
                slots[i].ClearSlot();
            }
        }
        for (int i = 0; i < eslots.Length; i++)
        {
            if (i < bEquipmnetInventory.items.Count)
            {
                eslots[i].AddItem(bEquipmnetInventory.items[i]);
            }
            else
            {
                eslots[i].ClearSlot();
            }
        }
    }
}
