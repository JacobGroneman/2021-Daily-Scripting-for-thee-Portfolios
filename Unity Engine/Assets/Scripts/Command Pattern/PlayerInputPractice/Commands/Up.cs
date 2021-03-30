using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : ICommand
{
    private readonly Transform _transform;
    private readonly float _velocity;
    
    public Up(Transform transform, float velocity)
    {
        this._transform = transform;
        this._velocity = velocity;
    }
        public void Execute()
        {
            _transform.Translate(Vector3.up * _velocity * Time.deltaTime);
        }
    
        public void Undo()
        {
            
        }
}
