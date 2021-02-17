using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld //Sealed accesses the que's singularly. 
{
    private static readonly GWorld _instance = new GWorld(); //Only one Version on Game World Applicable!\
    private static WorldStates _world;

    static GWorld()
    {
        _world = new WorldStates();
    }

    private GWorld()
    {
        
    }

    public static GWorld Instance
    {
        get {return _instance;}
    }

    public WorldStates GetWorld()
    {
        return _world;
    }
}
