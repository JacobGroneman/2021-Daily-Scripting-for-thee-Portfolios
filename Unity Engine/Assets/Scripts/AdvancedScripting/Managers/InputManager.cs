using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //(I'm creating this from scratch for optimization)

    private CharacterInventory _charInventory;

    void Start()
    {
        #region Initialization
        _charInventory = 
            GameObject.Find("Character").GetComponent<CharacterInventory>();
            #endregion
    }

    void Update()
    {
        CharacterInventoryInput();
    }

    #region Input
        void CharacterInventoryInput() //HotBarPresses
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _charInventory.TriggerItemUse(101);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _charInventory.TriggerItemUse(102);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _charInventory.TriggerItemUse(103);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _charInventory.TriggerItemUse(104);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                _charInventory.DisplayInventoryUI();
            }
        }
        #endregion
}
