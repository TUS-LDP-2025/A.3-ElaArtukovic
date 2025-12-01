using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public List<InventoryItem> inventory = new List<InventoryItem>();         //list of inventory items

    public event Action<InventoryItem> OnItemAdded;
    public event Action<InventoryItem> OnActiveItemChanged;



    [SerializeField]
    private int activeItemIndex = -1;      //keeps track of which is active, set to minus so its invalid - hasnt been set

    public void AddItem(CollectableItem itemToAddSO)   //to the class collectable item pass method add item and reference to scriptable objects we need to add                     
    {
        Debug.Log("Adding item " + itemToAddSO.ElementName);   //debug to see what is being added

        InventoryItem theInvetoryItem = inventory.Find(itemInList => itemInList.typeItem.ElementName == itemToAddSO.ElementName);   //pass the first item on list and check if the display name of it matches with the name we are looking for, if not move onto next one in list
                                                                                                                                    

        //item will be either null or equal to item already in the inventory
        if (theInvetoryItem != null)              //not null so we already have it, add quantity
        {
            theInvetoryItem.quantity++;           //if we pick up an item with a name from the list then add the quantity we have picked up
        }

        else
        {
            theInvetoryItem = new InventoryItem(itemToAddSO, 1);             //no inventory item so add one
                                                           //the scriptable object is now the item that is to be added 
                                                          //the quantity which we are adding
            inventory.Add(theInvetoryItem);                 //add to inventory list
        }

        //need to invoke event 
        // if (OnItemAdded != null)         //if there is an item we can add
        //{
        //OnItemAdded.Invoke(theInvetoryItem);      //pass item we have just added or updated             InventoryItem theInvetoryItem = inventory.Find(itemInList => itemInList.typeItem.ElementName == itemToAddSO.ElementName);
        //}

        OnItemAdded?.Invoke(theInvetoryItem);

        if (activeItemIndex == -1)       //if it hasnt been set yet
        {
            SetActiveItem(0);
        }

    }

    public void DropItem()
    {
        
        Debug.Log($"Dropping {inventory[activeItemIndex].typeItem.ElementName}");
    }

    private void SetActiveItem(int indexOfNewActiveItem)
    {
        if (indexOfNewActiveItem >= 0 && indexOfNewActiveItem < inventory.Count)
        {
            activeItemIndex = 0;           //set to valid, only thing in it
            InventoryItem theNewActiveItem = inventory[indexOfNewActiveItem];
            OnActiveItemChanged?.Invoke(theNewActiveItem);
        }
    }

}

[System.Serializable]
public class InventoryItem
{
    public CollectableItem typeItem;
    public int quantity;

    public InventoryItem(CollectableItem typeSO, int quantity)
    {
        this.typeItem = typeSO;
        this.quantity = quantity;
    }
}