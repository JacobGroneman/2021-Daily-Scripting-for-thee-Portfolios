using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sUIManager : MonoBehaviour
{
    private int _score;
    
    #region Singleton
        private static sUIManager _instance;
        public static sUIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    //Debug.Log("sUIManager is NULL!");
                    GameObject UIManagerObject = new GameObject("UI_Manager");
                    UIManagerObject.AddComponent<sUIManager>();
                }
    
                return _instance;
            }
        }
        #endregion
        
    private void Awake()
    {
        _instance = this;
    }
        public void UpdateScore(int score)
        {
            _score = score;
            Debug.Log("Score Updated.");
        }
}
