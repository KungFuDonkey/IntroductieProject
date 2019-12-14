using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetLives : MonoBehaviour
{
    public Item item;
    [SerializeField] private GameObject candyItem;
    [SerializeField] private GameObject candyTrigger;
    [SerializeField] private GameObject pokeballItem;
    [SerializeField] private GameObject pokeballTrigger;
    [SerializeField] private GameObject pechaBerryItem;
    [SerializeField] private GameObject pechaBerryTrigger;
    [SerializeField] private GameObject oranBerryItem;
    [SerializeField] private GameObject oranBerryTrigger;
    [SerializeField] private GameObject lumBerryItem;
    [SerializeField] private GameObject lumBerryTrigger;
    [SerializeField] private bool candy;
    [SerializeField] private bool pokebal;
    [SerializeField] private bool pechaBerry;
    [SerializeField] private bool oranBerry;
    [SerializeField] private bool lumBerry;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avatar") && candy == true)
        {
            
          
            candyItem.SetActive(false);
            candyTrigger.SetActive(false);
           

            HUD myHUD = other.gameObject.transform.parent.GetComponentInChildren<HUD>();
            myHUD.binventory.Add(item);
            //inventory.instance.Add(item);
        }

        else if (other.CompareTag("Avatar") && pokebal == true)
        {


            pokeballItem.SetActive(false);
            pokeballTrigger.SetActive(false);
            HUD myHUD = other.gameObject.transform.parent.GetComponentInChildren<HUD>();
            myHUD.binventory.Add(item);
            //inventory.instance.Add(item);
        }

        else if (other.CompareTag("Avatar") && pechaBerry == true)
        {


           pechaBerryItem.SetActive(false);
           pechaBerryTrigger.SetActive(false);


            inventory.instance.Add(item);
        }

        else if (other.CompareTag("Avatar") && oranBerry == true)
        {


            oranBerryItem.SetActive(false);
            oranBerryTrigger.SetActive(false);

            inventory.instance.Add(item);
        }
        else if (other.CompareTag("Avatar") && lumBerry == true)
        {


            lumBerryItem.SetActive(false);
            lumBerryTrigger.SetActive(false);

            inventory.instance.Add(item);
        }


    }



    

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }*/
}
