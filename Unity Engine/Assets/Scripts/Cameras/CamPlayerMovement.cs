using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlayerMovement : MonoBehaviour
{
    public float Velocity = 20;
    private Vector2 _motion;
    
    void Update()
    {
        _motion = new Vector2
            (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        transform.Translate(_motion * Velocity * Time.deltaTime);
    }
}
