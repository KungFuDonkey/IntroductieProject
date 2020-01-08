using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour 
{
    public static InventorySlot instance;

    [SerializeField] private GameObject tekst2;
    [SerializeField] private GameObject tekst;
   

    void Awake()
    {
        instance = this;
    }


    public Image picture;
    private Item _item;
    public float seconds;

    public void Start()
    {
        seconds = 10;
    }

    public Item Item
    {
        get { return _item; }
        set { _item = value;
        if (_item == null)
            {
                picture.enabled = false;
            }
            else { picture.sprite = _item.icon;
               picture.enabled = true;
            }
        }
    }
  


    public Button removebutton;

   

        public void AddItem (Item newItem)
    {
        Item = newItem;

        picture.sprite = Item.icon;
        picture.enabled = true;
        removebutton.interactable = true;
       
    }

    public void ClearSlot()
    {
        Item = null;

        picture.sprite = null;
        picture.enabled = false;
        removebutton.interactable = false;

    }
    public virtual void OnRemoveButton()
    {
        inventory.instance.Remove(Item);
    }

    public void Speedy()
    {
        tekst2.SetActive(false);
        inventory.instance.Remove(Item);
        fakemonBehaviour.instance.movementSpeed /= 3f;
    }
 

   
    public void WaitSpeed()
    {
       
        fakemonBehaviour.instance.movementSpeed *= 3f;
        
        tekst2.SetActive(true);
        TextCounterSpeed.instance.Start();
        TextCounterSpeed.instance.Update();
        Invoke("Speedy", seconds);
    }

    public void Jumpy()
    {

        fakemonBehaviour.instance.jumpspeed /= 3f;
        tekst.SetActive(false);
        inventory.instance.Remove(Item);

    }
    public void WaitJump()
    {
        fakemonBehaviour.instance.jumpspeed *= 3f;
        
        tekst.SetActive(true);
        TextCounterJump.instance.Start();
        TextCounterJump.instance.Update();
        Invoke("Jumpy", seconds);
    }




    public virtual void UseItem()
    {
        if (Item != null)
        {
            if (Item.name == "HealthCandy")
            {
                WaitJump();
            }
            else if (Item.name == "Lum Berry")
            {
                WaitSpeed();
               
            }
            
            Item.Use();

            //Item.RemoveItemFromInventory();
        }
    }
 

    
    
}
