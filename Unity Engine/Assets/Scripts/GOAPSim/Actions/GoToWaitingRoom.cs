using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingRoom : GAction
{
    void Start()
    {
        ActionName = "Go to Waiting Room";
        Cost = 1;
        Target = null;
        TargetTag = "WaitingArea";
        Duration = 0;
        Preconditions.Add("HasRegistered", 0);
        //dictionary AfterEffects.Add("IsWaiting", 0)
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