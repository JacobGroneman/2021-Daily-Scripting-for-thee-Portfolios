using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject PatientPrefab;
    public int PatientNumber;

    void Start()
    {
        for (int i = 0; i < PatientNumber; i++)
        {
            Instantiate(PatientPrefab, this.transform.position, Quaternion.identity);
        }
        
        Invoke("SpawnPatient", 5);
    }
    
        void SpawnPatient() 
        {
            Instantiate(PatientPrefab, this.transform.position, Quaternion.identity);
            Invoke("SpawnPatient", Random.Range(2, 10));
        }
}
