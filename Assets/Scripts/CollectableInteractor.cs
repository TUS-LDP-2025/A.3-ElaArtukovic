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
            inventoryManager.ItemRemoved();
            _inputs.drop = false;
        }
    }

   

    private void OnTriggerEnter(Collider other)        //if player collides with gameobjects
    {
        CollectableItem theItem;                                            //reference to class collectable item with the item
        theItem = other.GetComponent<CollectableController>().objectSO;       //the item being reference to scriptable object from collectable controller

        inventoryManager.ItemAdded(theItem);          //add the item to inventory manager
        Destroy(other.gameObject);                 //destroy the gameobject so it looks like we picked it up
    }
}
