using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    void Update()
    {
        
    }

    #region Weapon & Armor Changes
        void ChangeWeapon(ItemPickUp weaponPickUp)
        {
            if (!characterDefinition.unEquipWeapon
                (weaponPickUp, charInv, characterWeaponSlot))
            {
                characterDefinition.EquipWeapon
                    (weaponPickUp, charInv, characterWeaponSlot);
            }
        }
    
        void ChangeArmor(ItemPickup armorPickup)
        {
            if (!characterDefinition.unEquipArmor
                (armorPickUp, charInv));
            {
                characterDefinition.EquipArmor
                    (amorPickUp, charInv);
            }
        }
        #endregion
}
