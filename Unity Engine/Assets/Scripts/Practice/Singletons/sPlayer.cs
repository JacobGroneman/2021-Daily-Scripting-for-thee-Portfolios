using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPlayer : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SGameManager.Instance.DisplayName();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            sUIManager.Instance.UpdateScore(40);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            sSpawnManager.Instance.StartSpawning();
        }
    }
}
