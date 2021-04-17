using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlayerMovement3D : MonoBehaviour
{
    public float Velocity = 20;
    private Vector3 _motion;
    private Rigidbody _rb;

    void Start()
    {
        #region Initialize
            _rb = GetComponent<Rigidbody>();
            #endregion
    }
    
    void Update()
    {
        #region Movement
            _motion = new Vector3
                    (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            _rb.velocity = _motion * Velocity;
            #endregion
    }
}
