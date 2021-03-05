using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreated : GAction
{
    void Start()
    {
        ActionName = "Get Treated";
        Cost = 1;
        Target = null;
        TargetTag = null;
        Duration = 2;
        //Needs a Belief!
        //dictionary AfterEffects.Add("TreatPatient", 1)
        Agent = null;
        // bool Running
    }
        public override bool PrePerform()
        {
            Target = Inventory.FindItemWithTag("Cubicle");
    
            if (Target == null)
            {
                return false;
            }
    
            return true;
        }
    
        public override bool PostPerform()
        {
            GWorld.Instance.GetWorld().ModifyState("Treated", 1);
            
            Inventory.RemoveGameObject(Target);
            
            return true;
        }
}
