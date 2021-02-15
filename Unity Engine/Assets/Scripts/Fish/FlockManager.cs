using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject FishPrefab;
    public int NumberOfFish = 20;
    public GameObject[] Fishes;

    [Header("Fish Settings")] //Sections off variables in the Inspector!
    [Range(0.0f, 5.0f)] //Generates an Inspector Slider! (I want to research all these)
    public float MinVelocity;
    [Range(0.0f, 5.0f)] 
    public float MaxVelocity;
    [Range(1.0f, 10.0f)]
    public float NeighborDistance;
    [Range(0.0f, 5.0f)]
    public float RotationalVelocity;

    public Vector3 GoalPosition;
    [Range(0, 100f)]
    public float ChangePositionDynamicPercentage;
    
    public Vector3 SwimLimits = new Vector3(5, 5, 5);

    void Start()
    {
        Fishes = new GameObject[NumberOfFish];
        
        GoalPosition = this.transform.position;
        
        RandomizeFishStartPosition();
    }

    void Update()
    {
        DynamicChangePosition(ChangePositionDynamicPercentage);
    }
    
        private void RandomizeFishStartPosition()
        { //Randomizes start Position on All Fishes
          
            for (int i = 0; i < NumberOfFish; i++)
            {
                Vector3 pos = this.transform.position + 
                              new Vector3(Random.Range(-SwimLimits.x, SwimLimits.x),
                                  Random.Range(-SwimLimits.y, SwimLimits.y),
                                  Random.Range(-SwimLimits.z, SwimLimits.z));
                Fishes[i] = Instantiate(FishPrefab, pos, Quaternion.identity);
                Fishes[i].GetComponent<Flock>().FlockManager = this; //This is Genius (if organized)
            }
        }

        private void DynamicChangePosition(float percentageOfChance)
        {
            if (Random.Range(0, 100) < Mathf.RoundToInt(percentageOfChance)) //% of chance
            {
                GoalPosition = this.transform.position + 
                                new Vector3(Random.Range(-SwimLimits.x, SwimLimits.x),
                                            Random.Range(-SwimLimits.y, SwimLimits.y),
                                            Random.Range(-SwimLimits.z, SwimLimits.z));   
            }
        }
}
