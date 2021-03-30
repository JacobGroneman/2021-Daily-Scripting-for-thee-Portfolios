using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform _transform;
    private float _velocity = 10.0f;

    private ICommand 
        _up, _down, _left, _right;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _up = new Up(_transform, _velocity);
            _up.Execute();
        }
        if (Input.GetKey(KeyCode.S))
        {
            _down = new Down(transform, _velocity);
            _down.Execute();
        }
        if (Input.GetKey(KeyCode.A))
        {
            _left = new Left(transform, _velocity);
            _left.Execute();
        }
        if (Input.GetKey(KeyCode.D))
        {
            _right = new Right(transform, _velocity);
            _right.Execute();
        }
    }
}
