using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlayerTracker : MonoBehaviour
{
    public Transform TrackedObject;

    public float
        MaxDistance = 10f,
        MoveVelocity = 20f,
        UpdateVelocity = 10f;
    
    [Range(0, 10)] public float CurrentDistance = 5;
    private string _moveAxis = "Mouse ScrollWheel";
    
    private GameObject _ahead;
    
    private MeshRenderer _renderer;
    public float HideDistance = 1.5f;

    void Start()
    {
        #region Initialize
            _ahead = new GameObject("Ahead");
            _renderer = TrackedObject.gameObject.GetComponent<MeshRenderer>();
            #endregion
    }
    
    void Update()
    {
        #region Tracking
            _ahead.transform.position =
                TrackedObject.position + TrackedObject.forward
                * (MaxDistance * 0.25f); // 1/4 of Max
            
            CurrentDistance += 
                Input.GetAxisRaw(_moveAxis) * MoveVelocity * Time.deltaTime;
            
            CurrentDistance = Mathf.Clamp //Returns if in range
                (CurrentDistance, 0, MaxDistance);
            
            transform.position = Vector3.MoveTowards
                (transform.position,
                TrackedObject.position + Vector3.up * CurrentDistance
                - TrackedObject.forward * (CurrentDistance + MaxDistance * 0.5f), 
                UpdateVelocity * Time.deltaTime); //Jees! (I like these .MoveTowards)
            
            transform.LookAt(_ahead.transform);//Stabilize
            
            _renderer.enabled = (CurrentDistance > HideDistance);//Toggles 1st & 3rd person
            #endregion
    }
}
