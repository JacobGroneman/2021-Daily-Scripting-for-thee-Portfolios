using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameManager : MonoBehaviour
{
    #region Singleton
        private static SGameManager _instance;
        public static SGameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    //Debug.LogError("SGameManager is NULL!");
                    GameObject gameManagerObject = new GameObject("Game_Manager");
                    gameManagerObject.AddComponent<SGameManager>();
                }
                
                return _instance;
            }
        }
        #endregion
    
    private void Awake()
    {
        _instance = this;
    }
        public void DisplayName()
        {
            Debug.Log("The Name is Pickles Baxter!");
        }
    
}
