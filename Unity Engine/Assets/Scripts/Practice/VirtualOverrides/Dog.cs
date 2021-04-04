using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : vPet
{
    void Start()
    {
        #region Initialize
            _petName = "Dog";
            #endregion
    }

    void Update()
    {
        #region Input
        //Voice
            if (Input.GetKeyDown(KeyCode.S))
            {
                Speak(); //"Bark Y'all!"
            }
    
            if (Input.GetKeyDown(KeyCode.B))
            {
                base.Speak(); //"Voice" (I used this in the AI GOAP Simulation)
            }
            #endregion
    }
        protected override void Speak()
        {
            Debug.Log("Bark Y'all!");
        }
}
