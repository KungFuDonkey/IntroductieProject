
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
       
        if (name == "HealthCandy")
        {
            fakemonBehaviour.instance.jumpspeed *= 2f;
        }
        if (name == "Lum Berry")
        {
            fakemonBehaviour.instance.movementSpeed *= 2f;
        }
        if (name == "Oran Berry")
        {
            HealthBar.instance.CurrentHealth = HealthBar.instance.CurrentHealth + 20;
        }


    }

   

    public void RemoveItemFromInventory()
    {
        inventory.instance.Remove(this);
    }
}
