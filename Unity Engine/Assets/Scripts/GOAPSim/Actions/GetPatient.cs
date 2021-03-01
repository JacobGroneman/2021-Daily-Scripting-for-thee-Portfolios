using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{//Get Patient for Treatment
    void Start()
    {
        ActionName = "Get Patient";
        Cost = 1;
        Target = null;
        TargetTag = "Door";
        Duration = 2;
        Preconditions.Add("Waiting", 1);
        //dictionary AfterEffects.Add("TreatPatient", 1)
        Agent = null;
        // bool Running
    }
        public override bool PrePerform()
        {
            Target = GWorld.Instance.RemovePatient();

            if (Target == null)
            {
                return false;
            }
            return true;
        }
    
        public override bool PostPerform()
        {
            return true;
        }
}
