using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("TreatPatient", 1, true);
        Goals.Add(s1, 3);
    }
}
