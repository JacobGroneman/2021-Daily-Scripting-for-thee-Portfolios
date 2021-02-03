using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent _agent;

    private bool _activateSeek = false;
    private bool _activateFlee = false;

    public GameObject target;

    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Pursue();
    }

    private void Seek(Vector3 playerLocation)
    {
        _agent.SetDestination(playerLocation);
    }

    private void Flee(Vector3 playerLocation)
    {
        Vector3 fleeVector = playerLocation - this.transform.position;
        _agent.SetDestination(this.transform.position - fleeVector);
    }

    private void SeekFleeTesting()
    {
        Seek(target.transform.position);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _activateSeek = true;
            _activateFlee = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _activateSeek = false;
            _activateFlee = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _activateSeek = false;
            _activateSeek = false;
        }

        do
        {
            Seek(this.transform.position);
        } while (_activateSeek);

        do
        {
            Flee(this.transform.position);
        } while (_activateFlee);
    }

    private void Pursue()
    {
        Vector3 targetVector = target.transform.position - this.transform.position;
        float targetSpeed = target.GetComponent<sfDrive>().currentSpeed;
        
        float relativeVector = Vector3.Angle(this.transform.forward,
            this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetVector));
        
        
        if (toTarget > 90 && relativeVector < 20 || targetSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }
        
        float lookAhead = targetVector.magnitude / (_agent.speed + target.GetComponent<sfDrive>().currentSpeed);
        Seek(target.transform.position + target.transform.forward * (lookAhead * 5));
    }
}
