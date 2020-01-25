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
    [SerializeField] private GameObject Tekstmaxhealth;
    [SerializeField] private GameObject tekst3;
    public Image picture;
    private Item _item;
    public float seconds;
    public Button removebutton;

    void Awake()
    {
        instance = this;
    }

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
        tekst2.SetActive(false);
    }

    public void WaitSpeed()
    {
        ClientSend.AddEffects(3);
        tekst2.SetActive(true);
        inventory.instance.Remove(Item);
        TextCounterSpeed.instance.Start();
        TextCounterSpeed.instance.Update();
        Invoke("Speedy", seconds);
    }

    public void Jumpy()
    {
        tekst.SetActive(false);
    }

    public void WaitJump()
    {
        ClientSend.AddEffects(1);
        tekst.SetActive(true);
        inventory.instance.Remove(Item);
        TextCounterJump.instance.Start();
        TextCounterJump.instance.Update();
        Invoke("Jumpy", seconds);
    }

    public void Shield()
    {
        ClientSend.AddEffects(4);
    }

    public void Invy()
    {
        ClientSend.SetInvis(false);
        tekst3.SetActive(false);
    }

    public void WaitInvisible()
    {
        ClientSend.SetInvis(true);
        tekst3.SetActive(true);
        inventory.instance.Remove(Item);
        TextCounterInvisible.instance.Start();
        TextCounterInvisible.instance.Update();
        Invoke("Invy", seconds);
    }

    public void GetHealth()
    {
        if (HealthBar.instance.currentHealth >= 100)
        {
            WaitMaxHealth();
            return;
        }
        else
        {
            ClientSend.AddEffects(5);
            if (HealthBar.instance.currentHealth >= 100)
                HealthBar.instance.currentHealth = 100;
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
            else if (Item.name == "Pecha Berry")
            {
                inventory.instance.Remove(Item);
            }
            else if (Item.name == "Bandana")
            {
                Shield();
            }
        }
    }
}
