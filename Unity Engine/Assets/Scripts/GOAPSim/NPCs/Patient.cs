using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : GAgent
{ 
    new void Start() //(new is because GAgent has a Start())
    {
        base.Start();//GAgent inherited Start()
        
        //Sub Goals
            SubGoal s1 = new SubGoal("IsWaiting", 1, true);
            Goals.Add(s1, 3);
            
            SubGoal s2 = new SubGoal("IsTreated", 1, true);
            Goals.Add(s2, 5);
            
            SubGoal s3 = new SubGoal("IsHome", 1, true);
    }
}
