﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public Item item;
    public MiniMapCam MiniMap;
    public HealthBar healthBar;
    public VisualShield shieldBar;
    public GameObject Deathscreen, Winscreen, StormOverlay, BusCamera;
    public Transform itemsParent;
    public Transform gearParent;
    public GameObject jinventoryUI;
    public GameObject Spectator;
    public Text AlivePlayers;
    public EquipmentInventory jEquipmentInventory;
    public inventory binventory;
    public GameObject[] scoreboardItems = new GameObject[4];
    [HideInInspector]
    public List<GameObject> scores = new List<GameObject>();
    InventorySlot[] slots;
    EquipmentInventorySlot[] gearslots;

    void Awake()
    {
        instance = this;
    }
    
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
            GameManager.instance.freezeInput = !GameManager.instance.freezeInput;
            jinventoryUI.SetActive(!jinventoryUI.activeSelf);
            Cursor.visible = !Cursor.visible;
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Show scoreboard");
            instance.scoreboardItems[0].SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            instance.scoreboardItems[0].SetActive(false);
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
    }
}
