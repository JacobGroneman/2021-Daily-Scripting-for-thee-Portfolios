using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sSpawnManager : sMonoSingleton<sSpawnManager>
{
    /*#region Singleton
        private static sSpawnManager _instance;
        public static sSpawnManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    //Debug.Log("sSpawnManager is NULL!");
                    GameObject spawnManagerObject = new GameObject("Spawn_Manager");
                    spawnManagerObject.AddComponent<sSpawnManager>();
                }
    
                return _instance;
            }
        }
        #endregion*/
    public override void Initialize()
    {
        Debug.Log("Player Initialized");
    }
        public void StartSpawning()
        {
            Debug.Log("Spawn Started!");
        }
}
