using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameItem: MonoBehaviour
{
    public int id;
    public int itemNumber;
    public bool pickup = true;
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avatar"))
        {
            ClientSend.pickupItem(id, itemNumber);
        }
    }
}
