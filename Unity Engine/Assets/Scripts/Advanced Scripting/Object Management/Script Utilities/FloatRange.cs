using UnityEngine;

[System.Serializable]
public struct FloatRange
{
    public float Min, Max;

    public float RandomValueInRange
    {
        get {return Random.Range(Min, Max);}
    }
}
