using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : GAction
{
    void Start()
    {
        ActionName = "GoHome";
        Cost = 1;
        Target = null;
        TargetTag = "Home";
        Duration = 0;
        Preconditions.Add("IsTreated", 1);
        //AfterEffects.Add("IsHome", 1); //e.x. Patient.s3
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
