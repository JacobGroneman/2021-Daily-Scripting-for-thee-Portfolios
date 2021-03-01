using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld //Sealed accesses the que's singularly. 
{
    private static readonly GWorld _instance = new GWorld(); //Only one Version on Game World Applicable!\
    private static WorldStates _world;
    private static Queue<GameObject> _patients;

    static GWorld()
    {//Initializations
        _world = new WorldStates();
        _patients = new Queue<GameObject>();
    }

    private GWorld()
    {}

    public static GWorld Instance
    {
        get {return _instance;}
    }

    public WorldStates GetWorld()
    {
        return _world;
    }

    #region Character World Stats
    
        public void AddPatient(GameObject patient)
            {
                _patients.Enqueue(patient);
            }
        
        public GameObject RemovePatient()
        {
            if (_patients.Count == 0)
            {
                return null;
            }
            return _patients.Dequeue();
        }
            #endregion
   
}
