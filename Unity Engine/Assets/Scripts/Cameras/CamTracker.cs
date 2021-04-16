using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTracker : MonoBehaviour
{
    public Transform TrackedObject;
    
    public float UpdateVelocity = 3;
    
    public Vector2 TrackingOffset;
    private Vector3 _offset;

    void Start()
    {
        _offset = (Vector3)TrackingOffset; //I'm researching what "(Vector3)" does.

        _offset.z =
            transform.position.z - TrackedObject.position.z;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, TrackedObject.position + _offset, 
            UpdateVelocity * Time.deltaTime); //I likee
    }
}