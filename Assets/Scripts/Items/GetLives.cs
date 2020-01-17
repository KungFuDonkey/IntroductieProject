using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetLives : MonoBehaviour
{
    public Item item;
    public Equipment gear;
    [SerializeField] private GameObject candyItem;
    [SerializeField] private GameObject candyTrigger;
    [SerializeField] private bool candy;
    [SerializeField] private GameObject pokeballItem;
    [SerializeField] private GameObject pokeballTrigger;
    [SerializeField] private bool pokebal;
    [SerializeField] private GameObject pechaBerryItem;
    [SerializeField] private GameObject pechaBerryTrigger;
    [SerializeField] private bool pechaBerry;
    [SerializeField] private GameObject oranBerryItem;
    [SerializeField] private GameObject oranBerryTrigger;
    [SerializeField] private bool oranBerry;
    [SerializeField] private GameObject lumBerryItem;
    [SerializeField] private GameObject lumBerryTrigger;
    [SerializeField] private bool lumBerry;
    [Space]
    [SerializeField] private GameObject Bandana;
    [SerializeField] private GameObject BandanaTrigger;
    [SerializeField] private bool bandana;
    [SerializeField] private GameObject Bracelet;
    [SerializeField] private GameObject BraceletTrigger;
    [SerializeField] private bool bracelet;
    [SerializeField] private GameObject Pin;
    [SerializeField] private GameObject PinTrigger;
    [SerializeField] private bool pin;
 







    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Avatar") && candy == true)
        {
            if (Input.GetButtonDown("Pickup"))
            {
                candyItem.SetActive(false);
                candyTrigger.SetActive(false);
                
                inventory.instance.Add(item);
            }
        }

        else if (other.CompareTag("Avatar") && pokebal == true)
        {
            if (Input.GetButtonDown("Pickup"))
            {
              //  Player.instance.speedboost *= 3;
                pokeballItem.SetActive(false);
                pokeballTrigger.SetActive(false);
                /*HUD myHUD = other.gameObject.transform.parent.GetComponentInChildren<HUD>();
                myHUD.binventory.Add(item);*/
                inventory.instance.Add(item);
            }
        }

        else if (other.CompareTag("Avatar") && pechaBerry == true)
        {

            if (Input.GetButtonDown("Pickup"))
            {
                pechaBerryItem.SetActive(false);
                pechaBerryTrigger.SetActive(false);
                inventory.instance.Add(item);
            }
        }

        else if (other.CompareTag("Avatar") && oranBerry == true)
        {
            if (Input.GetButtonDown("Pickup"))
            {
                oranBerryItem.SetActive(false);
                oranBerryTrigger.SetActive(false);

                inventory.instance.Add(item);
            }
        }
        else if (other.CompareTag("Avatar") && lumBerry == true)
        {
            if (Input.GetButtonDown("Pickup"))
            {
                lumBerryItem.SetActive(false);
                lumBerryTrigger.SetActive(false);

                inventory.instance.Add(item);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avatar") && bandana == true)
        {
            Bandana.SetActive(false);
            BandanaTrigger.SetActive(false);
            //Player.instance.speedboost *= 3;

            EquipmentInventory.instance.Add(item);
            VisualShield.instance.currentShield += 20;
            if (VisualShield.instance.currentShield>= 100)
            {
                VisualShield.instance.currentShield = 100;
            }
        }

        else if (other.CompareTag("Avatar") && bracelet == true)
        {
            
        }
    }
}
