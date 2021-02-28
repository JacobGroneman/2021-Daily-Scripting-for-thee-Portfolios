﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHospital : GAction
{
    void Start()
    {
        ActionName = "Go to Hospital";
        Cost = 1;
        Target = null;
        TargetTag = "Door";
        Duration = 2;
        //Preconditions
        //dictionary AfterEffects.Add("HasArrived", 0)
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
