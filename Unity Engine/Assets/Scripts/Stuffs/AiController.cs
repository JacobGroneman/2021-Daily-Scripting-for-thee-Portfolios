using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    public GameObject Goal;
    private NavMeshAgent _agent;
    
    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        
        _agent.SetDestination(Goal.transform.position);
    }
}
