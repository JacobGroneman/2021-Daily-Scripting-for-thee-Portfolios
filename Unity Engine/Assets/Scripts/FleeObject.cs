using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeObject : MonoBehaviour
{
    public GameObject Obstacle;
    private GameObject[] _agents;
    
    void Start()
    {
        _agents = GameObject.FindGameObjectsWithTag("Agent");
    }

    void Update()
    {
        //Flee on Mouse Down
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                {
                    Instantiate
                        (Obstacle, hitInfo.point, Obstacle.transform.rotation);
                    foreach (var agent in _agents)
                    {
                        agent.GetComponent<AiCrowdController2>().DetectNewObstacle(hitInfo.point);
                    }
                }
            }
    }
}
