
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    public static Item instance;

    void Awake()
    {
        instance = this;
    }

    public virtual void Use()
    {
        if (name == "Pecha Berry")
        {
            inventory.instance.Remove(this);
        }
    }

    public void RemoveItemFromInventory()
    {
        inventory.instance.Remove(this);
    }
}
