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

    public bool UnEquipArmor() {}
    public bool EquipArmor() {}
}
