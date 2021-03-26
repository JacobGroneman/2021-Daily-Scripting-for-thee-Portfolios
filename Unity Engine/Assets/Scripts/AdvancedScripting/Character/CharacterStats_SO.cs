using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats_SO : MonoBehaviour
{ 
    public bool UnEquipWeapon
        (ItemPickup weaponPickup, CharacterInventory charInventory, GameObject weapon)
    {
        bool previousWeaponSame = false;

        if (weapon != null)
        {
            if (weapon == weaponPickup)
            {
                previousWeaponSame = true;
            }
            
            charInventory.inventoryDisplaySlots[2].sprite = null;
            DestroyObject(weaponSlot.transform.GetChild(0).gameObject); //Interesting Method
            weapon = null;
            currentDamage = baseDamage;
        }

        return previousWeaponSame;
    }

    public void EquipWeapon
        (ItemPickup weaponPickup, CharacterInventory charInventory, GameObject weapon)
    {
        Rigidbody newWeapon;

        weapon = weaponPickup;
        charInventory.inventoryDisplaySlots[2].sprite = weaponPickup.ItemDefinition.itemIcon;
        newWeapon = Instantiate(weaponPickup.ItemDefinition.weaponSlotObject, weaponSlot.transform);
        currentDamage = baseDamage + weapon.itemDefinition.itemAmount;
    }

    public bool EquipArmor(ItemPickup armorPickup, CharacterInventory charInventory)
    {
        switch (ArmorPickup.ItemDefinition.itemArmorSubType)
        {
            case ItemArmorSubType.Head:
                charInventory.inventoryDisplaySlots[3].sprite =
                    armorPickup.ItemDefinition.itemIcon;
                headArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Chest:
                charInventory.inventoryDisplaySlots[4].sprite =
                    armorPickup.ItemDefinition.itemIcon;
                chestArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Hands:
                charInventory.inventoryDisplaySlots[5].sprite =
                    armorPickup.ItemDefinition.itemIcon;
                handArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Legs:
                charInventory.inventoryDisplaySlots[6].sprite =
                    armorPickup.ItemDefinition.itemIcon;
                legArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Boots:
                charInventory.inventoryDisplaySlots[7].sprite =
                    armorPickup.ItemDefinition.itemIcon;
                footArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
        }
        return true;
    }
    public bool UnEquipArmor() {}
}
