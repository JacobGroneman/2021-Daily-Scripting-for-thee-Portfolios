using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TeachingMyFriend : MonoBehaviour
{
    //I'm teaching my friend how to script!
    //Let's Script a car!

    [SerializeField]
    private bool _isEngineOn;
    public GameObject Door;
    [SerializeField]
    private List<GameObject> _passengers;

    public int Velocity;
    public int RotationalVelocity = 45;

    void Start()
    {
        foreach (GameObject passenger in GameObject.FindGameObjectsWithTag("Person"))
        {
            _passengers.Add(passenger);
        }
    }

    void Update()
    {
        #region Input
        //Cars controls!
        if (_isEngineOn)
        {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Drive();
                }
    
                if (Input.GetKeyDown(KeyCode.S)) 
                {
                    Reverse();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    TurnLeft();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    TurnRight();
                }
        }
        #endregion
    }

    #region Input
        private void TurnLeft()
        {
            gameObject.transform.Rotate(Vector3.left * RotationalVelocity * Time.deltaTime);
        }
            private void TurnRight()
        {
            gameObject.transform.Rotate(Vector3.right * RotationalVelocity * Time.deltaTime);
        }
        private void Drive()
        {
            gameObject.transform.Translate(Vector3.forward);
        }
        private void Reverse()
        {
            gameObject.transform.Translate(Vector3.back);
        }
        #endregion
}
