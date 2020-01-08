﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HUD : MonoBehaviour
{

    public static HUD instance;

    void Awake()
    {
        instance = this;
    }

    public MiniMapCam MiniMap;
    public HealthBar healthBar;
    public GameObject Deathscreen;
    public Transform itemsParent;
    public Transform gearParent;
    public GameObject jinventoryUI;
   
    public GameObject Spectator;
    public Text AlivePlayers;

    public EquipmentInventory jEquipmentInventory;
    public inventory binventory;
 

    InventorySlot[] slots;
    EquipmentInventorySlot[] gearslots;

    void Start()
    {
        binventory = inventory.instance;
        jEquipmentInventory = EquipmentInventory.instance;


        binventory.onItemChangedCallback += UpdateUI;
        jEquipmentInventory.onItemChangedCallback += UpdateUI;
      
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        gearslots = gearParent.GetComponentsInChildren<EquipmentInventorySlot>();

        jinventoryUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            jinventoryUI.SetActive(!jinventoryUI.activeSelf);

            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < binventory.items.Count)
            {
                slots[i].AddItem(binventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        for (int i = 0; i < gearslots.Length; i++)
        {
            if (i < jEquipmentInventory.items.Count)
            {
                gearslots[i].AddItem(jEquipmentInventory.items[i]);
            }
            else
            {
                gearslots[i].ClearSlot();
            }
        }
    }
 
    public void ShowDeathscreen()
    {
        Deathscreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Spectate()
    {
        Instantiate(Spectator, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
    }

}
