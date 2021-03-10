using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : GAction
{
    void Start()
    {
        ActionName = "Rest";
        Cost = 1;
        Target = null;
        TargetTag = "BreakRoom";
        Duration = 5;
        Preconditions.Add("IsExhausted", 0);
        //AfterEffects.Add("IsRested", 1); //e.x. Patient.s3
        Agent = null;
        // bool Running
    }
        public override bool PrePerform()
        {
            return true;
        }
    
        public override bool PostPerform()
        {
            Beliefs.RemoveState("Exhausted");
            return true;
        }
}
