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

    private float _detectionRadius = 20f;
    private float _fleeRadius = 10f;

    void Start()
    { 
        _goals = GameObject.FindGameObjectsWithTag("Goal");
        
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_goals[Random.Range(0, _goals.Length)].transform.position);
        
        _animator = GetComponent<Animator>(); 
        _animator.SetFloat("walkingOffset", Random.Range(0 , 1));
        
        ResetAgent();
    }

    void Update()
    {
        OnGoalArrival();
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
    
    private void OnGoalArrival()
        {//resets Agent/Anim Stats and selects a new Agent Destination
            if (_agent.remainingDistance < 1)
            {
                ResetAgent();
                _agent.SetDestination(_goals[Random.Range(0, _goals.Length)].transform.position);
            }        
        }

    public void DetectNewObstacle(Vector3 clickPos)
        {
            if (Vector3.Distance(clickPos, this.transform.position) < _detectionRadius)
            {
                Vector3 fleeDirection = (this.transform.position - clickPos).normalized; //Opposite of Direction
                Vector3 newGoal = this.transform.position + (fleeDirection * _fleeRadius); //Place to run to
                
                NavMeshPath path = new NavMeshPath();
                _agent.CalculatePath(newGoal, path); //Assigns the flee goal: "newGoal", to path.
    
                if (path.status != NavMeshPathStatus.PathInvalid) //If the path is Accessible.
                {
                    _agent.SetDestination(path.corners[path.corners.Length - 1]);
                    
                    _animator.SetTrigger("isRunning");
                    _agent.speed = 10f;
                    _agent.angularSpeed = 500f;
                }
            }
        }
}
