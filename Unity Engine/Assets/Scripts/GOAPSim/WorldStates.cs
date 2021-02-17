using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldState
{
    public string Key; //World State (ex: "# of Free Cubicles)
    public int Value; //Index (ex. "5 Free Cubicles")
}

public class WorldStates
{ 
    public Dictionary<string, int> States;

    public WorldStates()
    {
        States = new Dictionary<string, int>();
    }
    
    public Dictionary<string, int> GetStates()
    {
        return States;
    }

    #region Conditions and Actions
    
        public bool HasState(string key)
        {
            return States.ContainsKey(key);
        }
        
        private void AddState(string key, int value)
        {
            States.Add(key, value);
        }
    
        private void RemoveState(string key)
        {
            if (States.ContainsKey(key))
            {
                States.Remove(key);
            }
        }
    
        public void ModifyState(string key, int value)
        {
            if (States.ContainsKey(key))
            {
                States[key] += value;
    
                if (States[key] <= 0)
                {
                    RemoveState(key);
                }
                else
                {
                    States.Add(key, value);
                }
            }
        }
        
        public void SetState(string key, int value)
        {
            if (States.ContainsKey(key))
            {
                States[key] = value;
            }
            else
            {
                States.Add(key, value);
            }
        }
        #endregion
}
