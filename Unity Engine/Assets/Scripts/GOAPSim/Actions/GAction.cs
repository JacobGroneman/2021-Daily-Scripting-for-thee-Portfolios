﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour //I Want Scriptable Object!!
{
    public bool Running = false;
    
    public string ActionName = "Action";
    public float Cost = 1.0f;
    public float Duration;

    public WorldState[] PreConditions; 
    public WorldState[] AfterEffects;
    public Dictionary<string, int> Preconditions; 
    public Dictionary<string, int> Effects;
    
    public WorldState AgentBeliefs;
    public WorldStates Beliefs;

    public GInventory Inventory;
    
    public GameObject Target;
    public string TargetTag;
    
    public NavMeshAgent Agent;

    public GAction() //Constructor
    {
        Preconditions = new Dictionary<string, int>();
        Effects = new Dictionary<string, int>();
    }

    public void Awake()
        {//Initialize
            Agent = this.gameObject.GetComponent<NavMeshAgent>(); //Find Alternative for Scriptable Object Class
            Inventory = this.GetComponent<GAgent>().Inventory;
            Beliefs = this.GetComponent<GAgent>().Beliefs;
    
            if (PreConditions != null)
            {
                foreach (WorldState w in PreConditions)
                {
                    Preconditions.Add(w.Key, w.Value);
                }
            }
            
            if (AfterEffects != null)
            {
                foreach (WorldState w in AfterEffects)
                {
                    Effects.Add(w.Key, w.Value);
                }
            }
        }
    
        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> p in Preconditions)
            {
                if (!conditions.ContainsKey(p.Key))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract bool PrePerform(); //For customizable conditions 
        public abstract bool PostPerform();
}
