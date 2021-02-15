using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager FlockManager;

    public float Velocity;
    public bool TurnBack = false;
    
    [Range(0, 100)]
    public float DynamicFlockVelocityPercentage;
    [Range(0, 100)]
    public float DynamicFlockRulesPercentage;
    
    void Start()
    {
        Velocity = Random.Range(FlockManager.MinVelocity, FlockManager.MaxVelocity);
    }

    void Update()
    {
        ReturnStrayFish();

        if (TurnBack == false)
        {
            DynamicBehavior(DynamicFlockVelocityPercentage, DynamicFlockRulesPercentage);
        }
        
        transform.Translate(0, 0, Velocity * Time.deltaTime);
    }

        void DynamicBehavior(float chanceOfDynamicVelocity, float chanceOfDynamicRules)
            //Utilized in ReturnStrayFish
        {
            if (Random.Range(0, 100) < Mathf.RoundToInt(chanceOfDynamicVelocity))
            {
                Velocity = Random.Range
                    (FlockManager.MinVelocity, FlockManager.MaxVelocity);
            }
            if (Random.Range(0, 100) < Mathf.RoundToInt(chanceOfDynamicRules))
            {
                ApplyFlockRules();
            }
        }
    
        private void ApplyFlockRules()
        {
            GameObject[] flockFish;
            flockFish = FlockManager.Fishes;

            Vector3 centerVector = Vector3.zero;
            Vector3 avoidVector = Vector3.zero;
            float globalVelocity = 0.01f; //These are group averages

            float neighborDistance;
            int groupSize = 0;

            foreach (GameObject fish in flockFish)
            {
                if (fish != this.gameObject)
                {
                    neighborDistance = Vector3.Distance
                    (fish.transform.position,
                        this.transform.position);
                    
                    if (neighborDistance <= FlockManager.NeighborDistance)
                    {
                        centerVector += fish.transform.position;
                        groupSize++;

                        if (neighborDistance < 1.0f)
                        {
                            avoidVector += (this.transform.position - fish.transform.position);
                        }

                        Flock otherFlock = fish.GetComponent<Flock>();
                        globalVelocity += otherFlock.Velocity;
                    }
                }
            }

            if (groupSize > 0)
            {
                centerVector = centerVector / groupSize + (FlockManager.GoalPosition - this.transform.position);
                Velocity = globalVelocity / groupSize;

                Vector3 flockDirection = (centerVector + avoidVector) - transform.position;

                if (flockDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp
                    (transform.rotation,
                        Quaternion.LookRotation(flockDirection),
                        FlockManager.RotationalVelocity * Time.deltaTime);
                }
            }
        }

        void ReturnStrayFish() //Returns any "out-of-bounds" fish to the bounds
        {
            Bounds b = new Bounds(FlockManager.transform.position, FlockManager.SwimLimits * 2);

            if (!b.Contains(transform.position))
            {
                TurnBack = true;
            }
            else
            {
                TurnBack = false;
            }

            if (TurnBack == true) //Directs out-of-bounds fish to the bound center
            {
                Vector3 toBoundsCenter = FlockManager.transform.position - this.transform.position;
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(toBoundsCenter), 
                    FlockManager.RotationalVelocity * Time.deltaTime);
            }
        }
}
