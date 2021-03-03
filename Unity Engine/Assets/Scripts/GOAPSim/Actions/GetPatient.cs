using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{
    private GameObject _resource;
    
    void Start()
    {//Get Patient for Treatment
        ActionName = "Get Patient";
        Cost = 1;
        Target = null;
        TargetTag = "Door";
        Duration = 2;
        Preconditions.Add("Waiting", 1);
        Preconditions.Add("FreeCubicle", 1);
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
            
            _resource = GWorld.Instance.RemoveCubicle();
                if (_resource != null)
                {
                    Inventory.AddItem(_resource);
                }
                else
                {//Turns away Patient if no cubicle is available
                    GWorld.Instance.AddPatient(Target);
                    Target = null;
                    return false;
                } 
            
            GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
            return true;
        }
    
        public override bool PostPerform()
        {
            GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
            
            if (Target)
            {
                //Adds the free Cubicle to Patient inventory
                Target.GetComponent<GAgent>().Inventory.AddItem(_resource);
            }
            return true;
        }
}
