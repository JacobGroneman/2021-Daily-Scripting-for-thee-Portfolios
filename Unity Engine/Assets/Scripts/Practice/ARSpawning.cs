using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSpawning : MonoBehaviour
{
    /*I didn't download the package for this content (ARFoundation)
     but evaluated the concepts to learn it*/
    
    [SerializeField] 
    private ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    [SerializeField] 
    private GameObject spawnablePrefab;

    private GameObject spawnedObject;

    void Start()
    {
        #region Initialize
            spawnedObject = null;
            #endregion
    }

    void Update()
    {
        #region Input
            if (Input.touchCount == 0)
                return;
        //If Touch:
            if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    SpawnPrefab(m_Hits[0].pose.position);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved 
                         && spawnedObject != null)
                {
                    spawnedObject.transform.position = m_Hits[0].pose.position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {//(Object isn't held)
                    spawnedObject = null;
                }
            }
            #endregion
    }
        public void SpawnablePrefab()
        {
            spawnedObject =
                Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        }
}
