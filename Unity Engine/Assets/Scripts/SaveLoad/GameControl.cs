using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    #region PseudoSingleton
        public static GameControl control;
        #endregion
    
    public float 
        health = 100, experience = 1200;
    
    void Awake()
    {
        #region PseudoSingleton
            if (control == null)
            {
                DontDestroyOnLoad(gameObject);
                control = this;
            }
            else if(control != this)
            {
                Destroy(this.gameObject);
            }
            #endregion
    }
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 30),
                "Health: " + health);
            GUI.Label(new Rect(10, 40, 150, 30),
                "Experience: " + experience);
        }
}
