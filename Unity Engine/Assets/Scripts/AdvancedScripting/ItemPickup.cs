using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemPickups_SO ItemDefinition;

    public CharacterStats charStats;
    private CharacterInventory charInventory;

    private GameObject foundStats;
    
    //Constructors

    void Start()
    {
        #region Initialize
            foundStats = GameObject.FindGameObjectWithTag("Player");
            charStats = foundStats.GetComponent<CharacterStats>();
            #endregion
    }
        void StoreItem()
        {
            charInventory.StoreItem(this);
        }

        public void UseItem()
        {
            switch (ItemDefinition.itemType)
            {
                case ItemTypeDefinitions.HEALTH:
                    charStats.ApplyHealth(ItemDefinition.itemAmount);
                    Debug.Log(CharStats.GetHealth());
                    break;
                case ItemTypeDefinitions.MANA:
                    charStats.ApplyMana(ItemDefinition.itemAmount);
                    break;
                case ItemTypeDefinitions.WEALTH:
                    charStats.GiveWealth(ItemDefinition.itemAmount);
                    break;
                case ItemTypeDefinitions.WEAPON:
                    //Change Weapon
                    charStats.ChangeWeapon();
                    break;
                case ItemTypeDefinitions.ARMOR:
                    //Change Armor
                    charStats.ChangeArmor();
                    break;
            }
        }
}
