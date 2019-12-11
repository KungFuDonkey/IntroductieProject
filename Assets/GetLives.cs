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
    [SerializeField] private bool candy;
    [SerializeField] private bool pokebal;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avatar") && candy == true)
        {
            
          
            candyItem.SetActive(false);
            candyTrigger.SetActive(false);

            inventory.instance.Add(item);
        }

        else if (other.CompareTag("Avatar") && pokebal == true)
        {


            pokeballItem.SetActive(false);
            pokeballTrigger.SetActive(false);

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
