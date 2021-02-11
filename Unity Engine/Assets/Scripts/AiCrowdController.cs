using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCrowdController : MonoBehaviour
{
    private GameObject[] _goals;
    
    private Animator _anim;
    private NavMeshAgent _agent;

    private float _speedMultiplier; 
    
    void Start()
    {
        //World Assignment
            _goals = GameObject.FindGameObjectsWithTag("Goal");
        //Component Assignment
            _anim = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        //Movement Assignment
            _anim.SetFloat("wOffset", Random.Range(0, 1));

            _speedMultiplier = Random.Range(0.5f, 2);
            _anim.SetFloat("speedMultiplier", _speedMultiplier);
            _agent.speed = _speedMultiplier;
        //Execution
            WalkToRandomGoal();
    }

    void Update()
    {
        //Destination Arrival Distance Buffer
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
