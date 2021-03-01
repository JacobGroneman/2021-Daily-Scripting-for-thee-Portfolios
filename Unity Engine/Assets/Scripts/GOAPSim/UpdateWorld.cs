using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    public Text States;
    
    void LateUpdate()
    {
        Dictionary<string, int> worldStates = 
            GWorld.Instance.GetWorld().GetStates();

        States.text = "";
        
        foreach (KeyValuePair<string, int> s in worldStates)
        {
            States.text += s.Key + "," + s.Value + "\n";
        }
    }
}
