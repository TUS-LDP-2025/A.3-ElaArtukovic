using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CollectableInteractor : MonoBehaviour           //handle interactions when we hit of objects, picking up and dropping
{

    public InventoryManager inventoryManager;      //reference to the inventory manager
    public StarterAssetsInputs _inputs;
    public CollectableItem theItem;                //reference to scriptable object
    private bool inRange = false;                                               // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        if (_inputs.drop == true)           //if RMB is pressed then drop item
        {
            inventoryManager.ItemRemoved();       //gets removed from inventory
            _inputs.drop = false;             
        }

       // if (_inputs.collect == true)          //if LMB is pressed then collect
        //{
           // inventoryManager.ItemAdded();      //gets added to inventory
           // _inputs.collect = false;
        //}
    }

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
        if (theItem != null)                   //if an iten can be collected/not null, add the item to collect to the inventory using InvMan logic
        {
            inventoryManager.ItemAdded(theItem);
            theItem = null;                       //reset it so more can be collected and it doesn't think the item i collected is still there
            inRange = false;
            Destroy(gameObject);
        }
    }

    private void DropItem()
    {
        inventoryManager.ItemRemoved();        //remove item from inv 
    }


    private void OnTriggerEnter(Collider other)        //if player collides with gameobjects
    {
                                                  
        theItem = other.GetComponent<CollectableController>().objectSO;       //the item being reference to scriptable object from collectable controller
        inRange = true;                            //if it collides then its in range
        //inventoryManager.ItemAdded(theItem);          //add the item to inventory manager        //this might be telling it to do it anytime it collides ughhhh
        //Destroy(other.gameObject);                 //destroy the gameobject so it looks like we picked it up
    }
}
