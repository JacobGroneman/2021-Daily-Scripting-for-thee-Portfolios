using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    new void Start() //"new" 'Cause of GAgent's Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("TreatPatient", 1, false);
        Goals.Add(s1, 3);
    }
}
