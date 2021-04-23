using UnityEngine;

[CreateAssetMenu]
public class vShapeFactory : ScriptableObject
{
    [SerializeField] 
    private vShape[] _prefabs;

    public vShape Get(int shapeID)
    {
        vShape instance = Instantiate
            (_prefabs[shapeID]);
            
        instance.ShapeID = shapeID;
            
        return instance;
    }
    
    public vShape GetRandom()
    {
        return Get
            (Random.Range(0, _prefabs.Length));
    }
}
