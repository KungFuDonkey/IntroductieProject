
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
        
       
       
         if (name == "Oran Berry")
        {
            HealthBar.instance.CurrentHealth += 25;
            if (HealthBar.instance.CurrentHealth > 100)
            {
                HealthBar.instance.CurrentHealth = 100;
            }
            inventory.instance.Remove(this);
        }

         else if (name == "Pokeball")
        {
            inventory.instance.Remove(this);
        }

         else if (name == "Pecha Berry")
        {
            inventory.instance.Remove(this);
        }

        /*else if (name == "HealthCandy")
        {
            fakemonBehaviour.instance.jumpspeed *= 2f;
        }
         if (name == "Lum Berry")
        {
            fakemonBehaviour.instance.movementSpeed *= 3f;
          
        }
         */
      


       
    }



    public void RemoveItemFromInventory()
    {
        inventory.instance.Remove(this);
    }

 
}
