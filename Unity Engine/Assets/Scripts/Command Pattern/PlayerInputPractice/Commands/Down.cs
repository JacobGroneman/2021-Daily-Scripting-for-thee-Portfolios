using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//I should make all these move commands generics. I need to review generics.....
public class Down : ICommand
{
    private readonly Transform _transform;
    private readonly float _velocity;
    
    public Down(Transform transform, float velocity)
    {
        this._transform = transform;
        this._velocity = velocity;
    }
        public void Execute()
        {
            _transform.Translate(Vector3.down * _velocity * Time.deltaTime);
        }
    
        public void Undo()
        {
            
        }
}