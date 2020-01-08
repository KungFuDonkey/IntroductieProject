using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gearTriggers : MonoBehaviour
{
    public Item item;
    [SerializeField] private GameObject Bandana;
    [SerializeField] private GameObject BandanaTrigger;
    [SerializeField] private bool bandana;
    [SerializeField] private GameObject Bracelet;
    [SerializeField] private GameObject BraceletTrigger;
    [SerializeField] private bool bracelet;
    [SerializeField] private GameObject Pin;
    [SerializeField] private GameObject PinTrigger;
    [SerializeField] private bool pin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avatar") && bandana == true)
        {
            Bandana.SetActive(false);
            BandanaTrigger.SetActive(false);

            HUD myHUD = other.gameObject.transform.parent.GetComponentInChildren<HUD>();
            myHUD.binventory.Add(item);
        }
      
        else if (other.CompareTag("Avatar") && bracelet == true)
        {

        }
    }
}
