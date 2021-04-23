using UnityEngine;

public class vShape : PersistableObject
{
    public int ShapeID
    {
        get {return _shapeID;}
        
        set
        {
            if (_shapeID == int.MinValue 
                && value != int.MinValue)
            {
                _shapeID = value;   
            }
            else
            {
                Debug.LogError("Not Allowed to Change ShapeID");
            }
        }
    }
        private int _shapeID = int.MinValue;
}
