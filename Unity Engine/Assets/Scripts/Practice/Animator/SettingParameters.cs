using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingParameters : MonoBehaviour
{
    private Animator _animator;
        //Parameters
            private static readonly int SpeedParameterHash //readonly = hash!
                = Animator.StringToHash("Speed");
        //States
            private static readonly int ShootStateHash
                = Animator.StringToHash("Shoot");
            private static readonly int WalkStateHash
                = Animator.StringToHash("Walk");
    void Start()
    {
        #region Initialize
            _animator = this.gameObject.GetComponent<Animator>();
            _animator.Play("Run", 0, 0.1f);
            #endregion
    }
    
    void Update()
    {
        _animator.SetFloat(SpeedParameterHash, 5.0f);
        
        #region Input
        //Shoot
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.PlayInFixedTime
                    (ShootStateHash, 1, 0f);
            }
        //Walk
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _animator.CrossFade
                    (WalkStateHash, 1f, 0, 0.1f);
                //(Hash, NormalizedTransitionDuration, Layer, NormalizedTimeOffset)
            }
            #endregion
            
        #region Dubug
        //Animator
            if (_animator.IsInTransition(0) || _animator.IsInTransition(1))
            {
                Debug.Log("Animator is in Transition!"); 
            }
            #endregion
    }
}
