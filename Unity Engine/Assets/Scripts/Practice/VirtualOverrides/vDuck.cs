using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vDuck : vPet
{
    void Start()
    {
        #region Initialize
            _petName = "Duck";
            #endregion
    }
        protected override void Speak()
        {
            Debug.Log("Quack Y'all!");
        }
}
