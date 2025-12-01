using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectableInteractor : MonoBehaviour           //handle interactions when we hit of objects, picking up and dropping
{

    public InventoryManager inventoryManager;      //reference to the inventory manager
    public StarterAssetsInputs _inputs;
    public CollectableItem theItem;                //reference to scriptable object
                                                   // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        if (_inputs.drop == true)
        {
            inventoryManager.DropItem();
            _inputs.drop = false;
        }
    }

    //public void RemoveItem(InventoryItem theItem)      
    //{
    // if (theItem != null)                //check if item isn't null
    //{
    // if (theItem.quantity > 1)             //if the current quantity of the item is bigger than one
    //{
    // theItem.quantity -= 1;            //take away one
    // }
    //else
    //{
    // theItem.quantity = 0;             //if its not bigger than 1 so equal to 1 and then removed, set quantity to 0
    //InventoryItem.Remove(theItem);    //remove the item from inventory
    // }
    // }                                                 //need to instantiate spawned item from prefab - get component from collectable controller - itemSO 
    //after instantiating need to get the component for setting color from collectable controller
    // }                                                     //need to invoke event 


    private void OnTriggerEnter(Collider other)        //if player collides with gameobjects
    {
        CollectableItem theItem;                                            //reference to class collectable item with the item
        theItem = other.GetComponent<CollectableController>().objectSO;       //the item being reference to scriptable object from collectable controller

        inventoryManager.AddItem(theItem);          //add the item to inventory manager
        Destroy(other.gameObject);                 //destroy the gameobject so it looks like we picked it up
    }
}
