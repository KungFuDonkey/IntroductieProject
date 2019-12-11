
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    
    new public string name = "New Item";
    public Sprite icon = null;

    public virtual void Use()
    {
        if (name == "CandyItem")
        {
            
        }
    }

    public void RemoveItemFromInventory()
    {
        inventory.instance.Remove(this);
    }
}
