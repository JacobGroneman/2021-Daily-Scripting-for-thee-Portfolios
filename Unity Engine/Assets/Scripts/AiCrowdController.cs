using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCrowdController : MonoBehaviour
{
    private GameObject[] _goals;
    
    private Animator _anim;
    private NavMeshAgent _agent;
    
    
    void Start()
    {
        _goals = GameObject.FindGameObjectsWithTag("Goal");
        
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        
        WalkToRandomGoal();
    }

    void Update()
    {
        if (_agent.remainingDistance < 1) //Remaining Distance to the Current Set Destination
        {
            _agent.SetDestination(_goals[Random.Range(0, _goals.Length)].transform.position);
        }
    }
    

    #region Behavior
    private void WalkToRandomGoal()
            {
                _anim.SetTrigger("isWalking");
                _agent.SetDestination(_goals[Random.Range(0, _goals.Length)].transform.position);
            }
            #endregion
}
