﻿using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentInventorySlot : InventorySlot
{
    public EquipmentType EquipmentType;

    public override void OnRemoveButton()
    {
        EquipmentInventory.instance.Remove(Item);
    }

    public override void UseItem()
    {
        if (Item.name == "Bandana")
        {
            Shield();
        }
        base.UseItem();
       
    }
}

