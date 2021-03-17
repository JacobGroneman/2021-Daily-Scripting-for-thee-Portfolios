using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemPickups_SO ItemDefinition;

    public CharacterStats CharStats;
    private CharacterInventory _charInventory;

    private GameObject _foundStats;
    
    //Constructors

    void Start()
    {
        #region Initialize
            _foundStats = GameObject.FindGameObjectWithTag("Player");
            CharStats = _foundStats.GetComponent<CharacterStats>();
            #endregion
    }
        void StoreItem()
        {
            //_charInventory.StoreItem();
        }

        public void UseItem()
        {
            switch (ItemDefinition.itemType)
            {
                case ItemTypeDefinitions.HEALTH:
                    CharStats.ApplyHealth(ItemDefinition.itemAmount);
                    Debug.Log(CharStats.GetHealth());
                    break;
                case ItemTypeDefinitions.MANA:
                    CharStats.ApplyMana(ItemDefinition.itemAmount);
                    break;
                case ItemTypeDefinitions.WEALTH:
                    CharStats.GiveWealth(ItemDefinition.itemAmount);
                    break;
                case ItemTypeDefinitions.WEAPON:
                    //Change Weapon
                    CharStats.ChangeWeapon();
                    break;
                case ItemTypeDefinitions.ARMOR:
                    //Change Armor
                    CharStats.ChangeArmor();
                    break;
            }
        }
}
