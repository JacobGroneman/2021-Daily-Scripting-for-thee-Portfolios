using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld //Sealed accesses the que's singularly. 
{
    private static readonly GWorld _instance = new GWorld(); //Only one Version on Game World Applicable!\
    
    private static WorldStates _world;
    
    private static Queue<GameObject> _patients;
    private static Queue<GameObject> _cubicles;

    #region Constructors & Singletons
        static GWorld()
        {//Initializations
            _world = new WorldStates();
            _patients = new Queue<GameObject>();
            //Cubicles
            _cubicles = new Queue<GameObject>();
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
                foreach (GameObject c in cubes) 
                { 
                    _cubicles.Enqueue(c); 
                }
                if (cubes.Length > 0)
                {
                    _world.ModifyState("FreeCubicle", cubes.Length);
                }
        }
    
        private GWorld()
        {}
    
        public static GWorld Instance
        {
            get {return _instance;}
        }
        #endregion
        
    public WorldStates GetWorld()
    {
        return _world;
    }

    #region World Queues
    
    //Patient Queue
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
        
    //Cubicle Queue
        public void AddCubicle(GameObject patient)
            {
                _cubicles.Enqueue(patient);
            }
                    
            public GameObject RemoveCubicle()
            {
                if (_cubicles.Count == 0)
                {
                    return null;
                }
                return _cubicles.Dequeue();
            }
            #endregion
}
