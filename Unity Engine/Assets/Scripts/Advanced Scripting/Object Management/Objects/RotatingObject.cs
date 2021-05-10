using UnityEngine;

public class RotatingObject : PersistableObject
{
    [SerializeField] 
    private Vector3 _angularVelocity; // deg./1 sec
    
    void FixedUpdate()
    {
        transform.Rotate(_angularVelocity * Time.deltaTime);
    }
}
