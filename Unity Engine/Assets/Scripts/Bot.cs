using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent _agent; 
    public GameObject target;
    //Seek() & Flee() values
        private bool _activateSeek = false;
        private bool _activateFlee = false; 
    //Wander() values
        float wanderRadius = 10f;
        float wanderDistance = 10f;
        float wanderJitter = 1f;
        float wanderTargetValue = Random.Range(-1.0f, 1.0f);

    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (CanSeeTarget())
        {
            SmartHide();
        }
    }

    
    #region Behavior

    Vector3 wanderTarget = Vector3.zero;
    private void Wander()
            {
                wanderTarget += new Vector3(wanderTargetValue * wanderJitter, 
                    0 , wanderTargetValue * wanderJitter);
                wanderTarget.Normalize();
                wanderTarget *= wanderRadius;
    
                Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
                Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);
                
                Seek(targetWorld);
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

    private bool CanSeeTarget()
            {
                RaycastHit raycastInfo;
                Vector3 rayToTarget = target.transform.position - this.transform.position;
                if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
                {
                    if (raycastInfo.transform.gameObject.tag == "Cop")
                    {
                        return true;
                    }
                }
                return false;
            }

    private void Hide()
            {
                float distance = Mathf.Infinity;
                Vector3 chosenSpot = Vector3.zero;
    
                for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
                {
                    Vector3 hidingVector = World.Instance.GetHidingSpots()[i].transform.position
                                              - target.transform.position;
                    Vector3 hidingPosition = World.Instance.GetHidingSpots()[i].transform.position
                                             + hidingVector.normalized * 10/*<--Distance from tree*/;
    
                    if (Vector3.Distance(this.transform.position, hidingPosition) < distance)
                    {
                        chosenSpot = hidingPosition;
                        distance = Vector3.Distance(this.transform.position, hidingPosition);
                    }
                }
                Seek(chosenSpot);
            }
    
    private void SmartHide()
            {
                float distance = Mathf.Infinity;
                Vector3 chosenSpot = Vector3.zero;
                Vector3 chosenDirection = Vector3.zero;
                GameObject chosenHidingSpot = World.Instance.GetHidingSpots()[0];
            
                for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
                {
                    Vector3 hidingVector = World.Instance.GetHidingSpots()[i].transform.position
                                           - target.transform.position;
                    Vector3 hidingPosition = World.Instance.GetHidingSpots()[i].transform.position
                                             + hidingVector.normalized * 10/*<--Distance from tree*/;
            
                    if (Vector3.Distance(this.transform.position, hidingPosition) < distance)
                    {
                        chosenSpot = hidingPosition;
                        chosenDirection = hidingVector;
                        chosenHidingSpot = World.Instance.GetHidingSpots()[i];
                        distance = Vector3.Distance(this.transform.position, hidingPosition);
                    }
                }

                Collider hidingSpotCollider = chosenHidingSpot.GetComponent<Collider>();
                Ray backRay = new Ray(chosenSpot, -chosenDirection.normalized);
                RaycastHit info; float castDistance = 100f;

                hidingSpotCollider.Raycast(backRay, out info, castDistance);
                
                Seek(info.point + chosenDirection.normalized * 2);
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
                
                float lookAhead = 
                    targetVector.magnitude / (_agent.speed + target.GetComponent<sfDrive>().currentSpeed);
                Seek(target.transform.position + target.transform.forward * (lookAhead * 5));
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
    #endregion
}
    