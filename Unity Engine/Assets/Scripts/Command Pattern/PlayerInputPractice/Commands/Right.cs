using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : ICommand
{
    private readonly Transform _transform;
    private readonly float _velocity;
    
    public Right(Transform transform, float velocity)
    {
        this._transform = transform;
        this._velocity = velocity;
    }
        public void Execute()
        {
            _transform.Translate(Vector3.right * _velocity * Time.deltaTime);
        }
    
        public void Undo()
        {
            
        }
}