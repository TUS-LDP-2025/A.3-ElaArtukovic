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

    public void ItemAdded(CollectableItem itemToAddSO)   //to the class collectable item pass method add item and reference to scriptable objects we need to add                     
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
            theInvetoryItem = new InventoryItem(itemToAddSO, 1);           //no item so add new and add for this quantity  
                                                         
            inventory.Add(theInvetoryItem);                 //add to inventory list
        }

        OnItemAdded?.Invoke(theInvetoryItem);

        if (activeItemIndex == -1)       //if it hasnt been set yet
        {
            SetActiveItem(0);
        }

    }

    public void ItemRemoved()
    {
        if (activeItemIndex < 0 || activeItemIndex >= inventory.Count)          //if the current selected index is less than 0 - has no active item         //greater or equal means its outside of list 
            return;
        InventoryItem itemToRemove = inventory[activeItemIndex];                //get the currently active item from inv list

        if (itemToRemove.typeItem.elementPrefab == null)                   //check if there is a prefab assigned, these are in element prefab on collectables in inspector
        {
            Debug.Log("No prefab assigned 4 this");
        }
        

        Vector3 dropPosition = transform.position + transform.forward * 1f;                         //drop it at the players position in front of them 
        Instantiate(itemToRemove.typeItem.elementPrefab, dropPosition, Quaternion.identity);        //instantiate to spawn a prefab of the boxes      //the item to remove is a collectable item - typeItem      //quanterion identity should have no rotations

        Debug.Log($"Dropping {inventory[activeItemIndex].typeItem.ElementName}");          

        itemToRemove.quantity--;           //remove quantity by one 

        if (itemToRemove.quantity <= 0)       
        {
            inventory.RemoveAt(activeItemIndex);             //remove the currently selected item from inv list

            if (inventory.Count == 0 )          //if the inv count is at 0, then active index should be -1 empty, should be selecting first item in list if at a 0
                activeItemIndex = -1;
            else
                activeItemIndex = 0;  

            if (activeItemIndex >= 0) 
            OnActiveItemChanged?.Invoke(inventory[activeItemIndex]);     //tell systems the active item changed
        }
    }

    private void SetActiveItem(int indexOfNewActiveItem)                
    {
        if (indexOfNewActiveItem >= 0 && indexOfNewActiveItem < inventory.Count)             //check if new index inside list range
        {
            {
                activeItemIndex = indexOfNewActiveItem;                                //changed from 0 cuz it sets it everytime, ignores passed index
                InventoryItem theNewActiveItem = inventory[indexOfNewActiveItem];      //gets item that will be the active one
                OnActiveItemChanged?.Invoke(theNewActiveItem);                         //tell systems active item changed
            }
        }
    }

}

[System.Serializable]
public class InventoryItem
{
    public CollectableItem typeItem;     //type item is reference to scriptable object in collectable item
    public int quantity;                 

    public InventoryItem(CollectableItem typeSO, int quantity)
    {
        this.typeItem = typeSO;                //the type item is equal to type scriptable object
        this.quantity = quantity;              
    }
}