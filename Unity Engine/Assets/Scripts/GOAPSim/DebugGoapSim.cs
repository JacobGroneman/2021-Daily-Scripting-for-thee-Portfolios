using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public static class DebugGoapSim
{
    public static void ModifyWorldSpeed() 
    {
        bool realtime = false; //When the Debug might throw bugs Lol
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && realtime == true)
        {
            Time.timeScale = 5;
            realtime = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && realtime == false)
        {
            Time.timeScale = 1;
            realtime = true;
        }
    }
}
