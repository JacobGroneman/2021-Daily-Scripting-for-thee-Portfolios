using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    public int TiredProbMin = 10;
    public int TiredProbMax = 20;

    new void Start() //"new" 'Cause of GAgent's Start()
    {
        base.Start();

        #region Sub Goals
            SubGoal s1 = new SubGoal("TreatPatient", 1, false);
            Goals.Add(s1, 3);
            
            SubGoal s2 = new SubGoal("IsRested", 1, false);
            Goals.Add(s2, 1);
            #endregion
            
        Invoke
            ("GetTired", Random.Range(TiredProbMin, TiredProbMax));
    }

        void GetTired()
        {
            Beliefs.ModifyState("IsExhausted", 0);
            
            Invoke
                ("GetTired", Random.Range(TiredProbMin, TiredProbMax));
        }
}
