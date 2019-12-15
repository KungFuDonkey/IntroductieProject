using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HUD : MonoBehaviour
{
    public MiniMapCam MiniMap;
    public HealthBar healthBar;
    public GameObject Deathscreen;
    public Transform itemsParent;
    public GameObject jinventoryUI;
    public GameObject Spectator;
    public Text AlivePlayers;

    public inventory binventory;

    InventorySlot[] slots;

    void Start()
    {
        binventory = inventory.instance;

        binventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
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
