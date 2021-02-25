using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sorry about the short code, I taught class until 9:30pm

public class PracticeGizmosHandles : MonoBehaviour
{
    public GameObject SampleManager;

    public float ShieldArea; //For Handle
    
    void Update()
    {
        OnDrawGizmosSelected(SampleManager, 10f);
    }

        void OnDrawGizmosSelected(GameObject manager, float gizmoSize)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(manager.transform.position, gizmoSize /*(Radius)*/);
        }
}
