using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{

    public Image picture;
    Item item;
    public Button removebutton;

    public void AddItem (Item newItem)
    {
        item = newItem;

        picture.sprite = item.icon;
        picture.enabled = true;
        removebutton.interactable = true;

    }

    public void ClearSlot()
    {
        item = null;

        picture.sprite = null;
        picture.enabled = false;
        removebutton.interactable = false;

    }
    public void OnRemoveButton()
    {
        inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            item.RemoveItemFromInventory();
            
           
        }
    }
}
