using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCrowdController2 : MonoBehaviour
{
    private GameObject[] _goals;

    private NavMeshAgent _agent;
    private Animator _animator;
        private float _speedMultiplier;

    void Start()
    { 
        _goals = GameObject.FindGameObjectsWithTag("Goal");
        
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_goals[Random.Range(0, _goals.Length)].transform.position);
        
        _animator = GetComponent<Animator>(); 
        _animator.SetFloat("walkingOffset", Random.Range(0 , 1));
        
        ResetAgent();
    }
        
    private void ResetAgent()
        {
            //Value Assignments
                _speedMultiplier = Random.Range(0.1f, 1.5f);
                _agent.speed = _speedMultiplier * 2;
                _agent.angularSpeed = 120f;
                _animator.SetFloat("SpeedMultiplier", _speedMultiplier);
            //Animation Assignment
                _animator.SetTrigger("isWalking");
            //Clears Agent Destination
                _agent.ResetPath();
        }
}
