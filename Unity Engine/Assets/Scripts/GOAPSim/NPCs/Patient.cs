﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : GAgent
{
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("isWaiting", 1, true);
        Goals.Add(s1, 3);
    }
}