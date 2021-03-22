using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;S

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

        void AddItemToHotBar(InventoryEntry itemForHotBar)
        {
            int hotBarCounter = 0;
            bool increaseCount = false;
            
            //Open Hot Bar Slots:
            foreach (Image images in hotBarDisplayHolders)
            {
                hotBarCounter += 1;

                if (itemForHotBar.hotBarSlot == 0)
                {
                    if (images.sprite == null)
                    {
                        //adds item to hotBar
                        itemForHotBar.hotBarSlot = hotBarCounter;
                        //show sprite in hotBar
                        images.sprite = itemForHotBar.hbSprite;
                        increaseCount = true;
                        break;
                    }
                }
                else if (itemForHotBar.invEntry.itemDefinition.isStackable)
                {
                    increaseCount = true;
                }
            }

            if (increaseCount)
            {
                hotBarDisplayHolders[itemForHotBar.hotBarSlot - 1].GetComponentInChildren<Text>().text
                    = itemForHotBar.stackSize.ToString();
            }
            
            increaseCount = false;
        }

        void DisplayInventoryUI()
        {
            if (InventoryDisplayHolder.activeSelf == true)
            {
                InventoryDisplayHolder.SetActive(false);
            }
            else
            {
                InventoryDisplayHolder.SetActive(true); //I like this toggle method!
            }
        }

        void FillInventoryDisplay()
        {
            int slotCounter = 9;

            foreach (var VARIABLE in KeyValuePair<int, InventoryEntry> ie in itemsInDictionary)
            {
                slotCounter += 1;
                InventoryDisplaySlots[slotCounter].sprite = ie.Value.hbSprite;
                ie.Value.inventorySlot = slotCounter - 9;
            }
            while (slotCounter < 29)
            {
                slotCounter++;
                InventoryDisplaySlots[slotCounter].sprite = null;
            }
        }

        void TriggerItemUse(int itemToUseID)
        {
            bool triggerItem = false;

            foreach (KeyValuePair<int, InventoryEntry> ie in itemsInInventory)
            {//HotBarSlots are more than 100
                if (itemToUseID > 100)
                {
                    itemToUseID -= 100;

                    if (ie.Value.hotBarSlot == itemToUseID)
                    {
                        triggerItem = true;
                    }
                }
                else
                {//less than 100 (Inventory Slots)
                    if (ie.Value.inventorySlot == itemToUseID)
                    {
                        triggerItem = true;
                    }
                }

                if (triggerItem)
                {
                    if (ie.Value.stackSize == 1)
                    {
                        if (ie.Value.inventorySlot.itemDefinition.isStackable)
                        {
                            if (ie.Value.hotBarSlot != 0)
                            {
                                hotBarDisplayHolders[ie.Value.hotBarSlot - 1].sprite = null;
                                hotBarDisplayHolders[ie.Value.hotBarSlot - 1]
                                    .GetComponentInChildren<Text>().text = "0";
                            }

                            ie.Value.invEntry.UseItem();
                            itemsInInventory.Remove(ie.Key);
                            break;
                        }
                        else
                        {//not stackable
                            ie.Value.invEntry.UseItem();
                            if (!ie.Value.invEntry.itemDefinition.isIndestructable)
                            {
                                itemsInInventory.Remove(ie.Key);
                                break;
                            }
                        }
                    }
                    else //!triggerItem
                    {
                        ie.Value.invEntry.UseItem();
                        ie.Value.stackSize -= 1;
                        hotBarDisplayHolders[ie.Value.hotBarSlot - 1]
                            .GetComponentInChildren<Text>().text = ie.Value.stackSize.ToString();
                        break;
                    }
                }
            }
            
            FillInventoryDisplay();
        }
}
