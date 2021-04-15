using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScipt : MonoBehaviour
{
    private bool _isHungry = true;
    public GameObject Food;
    private GameObject[] _stomach;

    private string hungryMessage = "I want them burgers!";
    private string fullMessage = "I got that tasty";

    void Update()
    {
        if (_stomach.Length == 1)
        {
            _isHungry = false;
            Debug.Log(fullMessage);
        }
        if (_isHungry)
        {
            _stomach[0] = Food;
            Debug.Log(fullMessage);
        }

        if (Math.Abs(Time.time - (60 * 60) * 4) < 0)
        {
            _isHungry = true;
        }
    }
}
