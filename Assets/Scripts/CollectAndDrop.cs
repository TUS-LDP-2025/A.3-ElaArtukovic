using UnityEngine;
using UnityEngine.InputSystem;

public class CollectAndDrop : MonoBehaviour
{
    public InventoryManager invManager;
    private CollectableItem itemTC;             //named it TC for TO COLLECT
    private bool inRange = false;            //need for player check 

    public void OnCollect(InputAction.CallbackContext context)          //set LMB for collecting and RMB for dropping
    {
        if (context.performed && inRange)             //if LMB is presed and the player is in range then collect item
        {
            CollectItem();
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.performed)               //if RMB is pressed then drop item
        {
            DropItem();
        }
    }

    private void CollectItem()
    {
        if (itemTC != null)                   //if item is collected and its not null, add the item to collect to the inventory 
        {
            invManager.ItemAdded(itemTC);
            itemTC = null;
            inRange = false;
        }
    }

    private void DropItem()
    {
        invManager.ItemRemoved();
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectableItem collectable = other.GetComponent<CollectableItem>();
        if (collectable != null)
        {
            itemTC = collectable;
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CollectableItem collectable = other.GetComponent<CollectableItem>();
        if (collectable == itemTC)
        {
            itemTC = null;
            inRange = false;
        }
    }


}
