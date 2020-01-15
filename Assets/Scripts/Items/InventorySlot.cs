﻿using UnityEngine.UI;
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
    [SerializeField] private GameObject Tekstmaxhealth;
    [SerializeField] private GameObject tekst3;


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

    public void MaxHealth()
    {
        Tekstmaxhealth.SetActive(false);
    }

    public void WaitMaxHealth()
    { 
        Tekstmaxhealth.SetActive(true);
        Invoke("MaxHealth", 2);
    }

    public void Speedy()
    {
        ClientSend.UseItem(3);
        tekst2.SetActive(false);
        inventory.instance.Remove(Item);
    }
    public void WaitSpeed()
    {
        ClientSend.UseItem(2);
        tekst2.SetActive(true);
        TextCounterSpeed.instance.Start();
        TextCounterSpeed.instance.Update();
        Invoke("Speedy", seconds);
    }

    public void Jumpy()
    {
        ClientSend.UseItem(1);
        tekst.SetActive(false);
        inventory.instance.Remove(Item);

    }
    public void WaitJump()
    {
        ClientSend.UseItem(0);
        tekst.SetActive(true);
        TextCounterJump.instance.Start();
        TextCounterJump.instance.Update();
        Invoke("Jumpy", seconds);
    }

    public void Invy()
    {
        ClientSend.UseItem(5);
        tekst3.SetActive(false);
        inventory.instance.Remove(Item);

    }
    public void WaitInvisible()
    {
        ClientSend.UseItem(4);
        tekst3.SetActive(true);
        TextCounterInvisible.instance.Start();
        TextCounterInvisible.instance.Update();
        Invoke("Invy", seconds);
    }

    public void GetHealth()
    {
        if (HealthBar.instance.CurrentHealth >= 100)
        {
            WaitMaxHealth();
            return;
        }
        else
        {
            HealthBar.instance.CurrentHealth += 20;
            if (HealthBar.instance.CurrentHealth >= 100)
                HealthBar.instance.CurrentHealth = 100;
            inventory.instance.Remove(Item);
        }
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
            else if (Item.name == "Oran Berry")
            {
                GetHealth();
            }
            else if (Item.name == "Pokeball" || Item.name == "Pokeball2")
            {
                WaitInvisible();
            }

            Item.Use();
        }
    }
 

    
    
}
