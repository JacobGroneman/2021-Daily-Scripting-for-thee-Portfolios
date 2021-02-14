using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager FlockManager;

    public float Velocity;
    
    void Start()
    {
        Velocity = Random.Range(FlockManager.MinVelocity, FlockManager.MaxVelocity);
    }

    void Update()
    {
        ApplyFlockRules();
        transform.Translate(0, 0, Velocity * Time.deltaTime);
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
                centerVector /= groupSize;
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
}
