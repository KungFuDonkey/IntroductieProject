
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
   
    public static Item instance;

    void Awake()
    {
        instance = this;
    }

    new public string name = "New Item";
    public Sprite icon = null;

 

    public virtual void Use()
    {
         if (name == "Pokeball")
        {
            inventory.instance.Remove(this);
        }

         else if (name == "Pecha Berry")
        {
            inventory.instance.Remove(this);
        }
    }



    public void RemoveItemFromInventory()
    {
        inventory.instance.Remove(this);
    }

 
}
