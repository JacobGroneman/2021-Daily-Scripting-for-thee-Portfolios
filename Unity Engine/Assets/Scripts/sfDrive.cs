using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfDrive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float currentSpeed = 0.0f;
    
    void Update()
    {
        //Get the Input Axis. (arrow keys) values are 1 or -1
        
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        //Converts 10 meters/Frame to 10 meters/second
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        
        //Translates across the z-axis
        transform.Translate(0, 0, translation);
        currentSpeed = translation;
        
        //Y-axis rotation
        transform.Rotate(0, rotation, 0);
    }
}
