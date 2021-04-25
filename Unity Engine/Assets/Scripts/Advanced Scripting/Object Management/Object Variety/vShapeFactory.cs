using UnityEngine;

[CreateAssetMenu]
public class vShapeFactory : ScriptableObject
{
    [SerializeField] 
    private vShape[] _prefabs;

    [SerializeField] 
    private Material[] _materials;

    public vShape Get(int shapeID = 0, int materialID = 0)
    {
        vShape instance = Instantiate
            (_prefabs[shapeID]);
        
        instance.ShapeID = shapeID;
            instance.SetMaterial(_materials[materialID], materialID);
            
        return instance;
    }
    
    public vShape GetRandom()
    {
        return Get
            (Random.Range(0, _prefabs.Length),
            Random.Range(0, _materials.Length));
    }
}
