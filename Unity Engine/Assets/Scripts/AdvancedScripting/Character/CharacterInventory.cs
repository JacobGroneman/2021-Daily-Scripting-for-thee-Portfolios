using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    void Update()
    {
        if (!addedItem)
        {
            TryPickUp();
        }
    }

        void StoreItem(itemPickUp itemToStore)
        {
            AddedItem = false;

            if ((charStats.characterDefinition.currentEncumbrance +
                 itemToStore.itemDefinition.itemWeight) <= charStats.characterDefinition.maxEncumbrance)
            {
                itemEntry.InvEntry = itemToStore;
                itemEntry.StackSize = 1;
                itemEntry.hbSprite = itemToStore.itemDefinition.itemIcon;

                itemToStore.gameObject.SetActive(false);
            }
        }

        void TryPickUp()
        {
            bool inInventory = true;

            if (itemEntry.invEntry)
            {
                //Item is not in Inventory:
                if (itemsInInventory.Count == 0)
                {
                    addedItem = AddItemToInv(addedItem);
                }
                //Item is already in Inventory:
                else
                {
                    //Item is Stackable:
                    if (itemEntry.invEntry.itemDefinition.isStackable)
                    {
                        foreach (KeyValuePair<int, InventoryEntry> ie in itemsInInventory)
                        {//Item already in Inventory:
                            if (itemEntry.invEntry.itemDefinition == ie.Value.invEntry.itemDefinition)
                            {//Stack++ and Destroy new instance
                                ie.Value.stackSize += 1;
                                AddItemToHotBar(ie.Value);
                                inInventory = true;
                                DestroyObject(itemEntry.invEntry.gameObject); //Check this
                                break;//foreach break
                            }
                            else
                            {
                                inInventory = false; //Just a safety feature I guess
                            }
                        }
                    }
                    //If item is Stackable:
                    else
                    {
                        inInventory = false;
                        
                        //Maxed out Inventory Condition:
                        if (itemsInInventory.Count == inventoryItemCap)
                        {
                            itemEntry.invEntry.gameObject.Setactive(true);
                            Debug.Log("Inventory is Full!");
                        }
                    }
                    //If Inventory has space:
                    if (!inInventory)
                    {
                        addedItem = AddItemToInv(addedItem);
                        inInventory = true;
                    }
                }
            }
        }
        //For Newly Added Items
        bool AddItemToInv(bool finishedAdding)
        {
            itemsInInventory.Add
            (idCount, new InventoryEntry
            (itemEntry.stackSize, Instantiate(itemEntry.invEntry), itemEntry.hbSprite));
            
            DestroyObject(itemEntry.invEntry.gameObject);
            
            FillInventoryDisplay();
            AddItemToHotBar(itemsInInventory[idCount]);

            idCount = IncreaseID(idCount);
            
            //Reset Item Entry
            
            finishedAdding = true;
            return finishedAdding; //Their Code is a tad wonky Lol!!!
        }

        int IncreaseID(int currentID)
        {
            int newID = 1;

            for (int itemCount = 1; itemCount <= itemsInInventory.Count; itemCount++)
            {
                if (itemsInInventory.ContainsKey(newID))
                {
                    newID++;
                }
                else return newID;
            }

            return newID;
        }
    
        void AddItemToHotBar(InventoryEntry itemForHotBar) {}

        void DisplayInventory() {}
        
        void FillInventoryDisplay() {}
        
        void TriggerItemUse(int itemToUseID) {}
}
