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
        #region Input
        //Movement
            if (Input.GetKey(KeyCode.W))
            {
                _up = new Up(_transform, _velocity);
                _up.Execute();
                InputCommandManager.Instance.AddCommandToBuffer(_up);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _down = new Down(transform, _velocity);
                _down.Execute();
                InputCommandManager.Instance.AddCommandToBuffer(_down);
            }
            if (Input.GetKey(KeyCode.A))
            {
                _left = new Left(transform, _velocity);
                _left.Execute();
                InputCommandManager.Instance.AddCommandToBuffer(_left);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _right = new Right(transform, _velocity);
                _right.Execute();
                InputCommandManager.Instance.AddCommandToBuffer(_right);
            }    
        //Replay/Rewind Movement
            if (InputCommandManager.Instance.IsCoroutineRunning == false)
            {
                //Replay
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        InputCommandManager.Instance.ReplayCommandBuffer();
                    }
                //Rewind
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        InputCommandManager.Instance.RewindCommandBuffer();
                    } 
            }
            #endregion
    }
}
