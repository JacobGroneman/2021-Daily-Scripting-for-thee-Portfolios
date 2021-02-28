using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : GAction
{
    void Start()
    {
        ActionName = "Register";
        Cost = 1;
        Target = null;
        TargetTag = "Reception";
        Duration = 5;
        Preconditions.Add("HasArrived", 0);
        //dictionary AfterEffects.Add("HasRegistered", 0)
        Agent = null;
        // bool Running
    }
        public override bool PrePerform()
        {
            return true;
        }
    
        public override bool PostPerform()
        {
            return true;
        }
}