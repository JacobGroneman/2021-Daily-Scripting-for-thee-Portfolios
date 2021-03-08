using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCubicle : GAction
{
    void Start()
    {//Get Patient for Treatment
        ActionName = "Go To Cubicle";
        Cost = 1;
        Target = null;
        TargetTag = null;
        Duration = 2;
        Preconditions.Add("PatientPickedUp", 1);
        //AfterEffects.Add("TreatPatient", 1)
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
            GWorld.Instance.GetWorld().ModifyState("TreatingPatient", 1);
            GWorld.Instance.AddCubicle(Target);
            Inventory.RemoveGameObject(Target);
            GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
            
            return true;
        }
}
