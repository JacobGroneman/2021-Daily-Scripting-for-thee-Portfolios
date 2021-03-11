using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewCalcDistance : MonoBehaviour
{//We are pointA
    public GameObject PointB;
    
    void Update()
    {
        #region Input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CalculateDistanceManual();
            }
    
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CalculateDistanceUnity();
            }
            #endregion
    }

    #region Calculation
        void CalculateDistanceManual()
        {
            Vector3 pointAPos = this.transform.position;
            Vector3 pointBPos = PointB.transform.position;
    
            float distance =
                Mathf.Sqrt(Mathf.Pow(pointAPos.x - pointBPos.x, 2) +
                              Mathf.Pow(pointAPos.y - pointBPos.y, 2) +
                              Mathf.Pow(pointAPos.z - pointBPos.z, 2));
    
            Debug.Log("Distance from Point A to Point B is: " + distance);
        }
        void CalculateDistanceUnity()
        {
            Vector3 pointAPos = this.transform.position;
            Vector3 pointBPos = PointB.transform.position;
    
            float distance = Vector3.Distance(pointAPos, pointBPos);
            
            Debug.Log("Distance from Point A to Point B is: " + distance);
        }
        #endregion
}
