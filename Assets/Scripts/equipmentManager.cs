using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipmentManager : MonoBehaviour
{
    Item item;
    public static equipmentManager instance;

    void Awake()
    {
        instance = this;
    }



    /*
    public static equipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    Equipment[] currentEquipment;
    EquipmentInventory jinventory;

    void Start()
    {
        jinventory = EquipmentInventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentInventorySlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipmentType;
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            jinventory.Add(oldItem);
        }
        currentEquipment[slotIndex] = newItem;
    }
    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment olditem = currentEquipment[slotIndex];
            jinventory.Add(olditem);
            currentEquipment[slotIndex] = null;
        }
    }

    public void UnequipAll()
    {
        for (int i=0; i<currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }*/
}

