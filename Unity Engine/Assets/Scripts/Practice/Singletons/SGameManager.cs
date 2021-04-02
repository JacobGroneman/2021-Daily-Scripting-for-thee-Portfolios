using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameManager : MonoBehaviour
{
    private static SGameManager _instance;
    
    public static SGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SGameManager._instance = NULL!");
            }
            
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

        public void DisplayName()
        {
            Debug.Log("The Name is Pickles Baxter!");
        }
    
}
