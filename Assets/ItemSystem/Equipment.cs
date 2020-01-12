using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType EquipmentType;
    public int shieldModifier;
    public int speedModifier;
    public EquipmentInventory equipmentInventory;






    public override void Use()
    {
       

        //if (name == "Bandana")
        //{
         //   base.Use();
            //equipmentManager.instance.Equip(this);
            //equipmentInventory.Add(this);
        //    RemoveItemFromInventory();
       // }
        
      //  else
      //  {
           base.Use();
      //  }
    }
    
}
public enum EquipmentType {Bandana, Pin, Bracelet, Hat}