using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public EquipmentType EquipmentType;
    public static EquipmentInventory instance;
    EquipmentInventorySlot invslot;

    void Awake()
    {
        instance = this;
    }



    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        
            if (items.Count >= space)
            {
                return;
            }
            items.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    











    /*
     [SerializeField] Transform equipmentSlotsParent;
     [SerializeField] EquipmentInventorySlot[] equipmentslots;

     private void OnValidate()
     {
         equipmentslots = equipmentSlotsParent.GetComponentsInChildren<EquipmentInventorySlot>();
     }


     public void AddItem(Equipment equip) //out Equipment previousequip)
     {
         for (int i=0; i< equipmentslots.Length; i++)
         {
             if (equipmentslots[i].EquipmentType == equip.EquipmentType)
             {
                // previousequip = (Equipment)equipmentslots[i].Item;
                equipmentslots[i].Item = equip;
                // return true;
             }
         }
        // previousequip = null;
        // return false;

     }

     public void RemoveItem(Equipment equip)
     {
         for (int i = 0; i < equipmentslots.Length; i++)
         {
             if (equipmentslots[i].Item == equip)
             {
                 equipmentslots[i].Item = null;

             }
         }

     }

    */
}
