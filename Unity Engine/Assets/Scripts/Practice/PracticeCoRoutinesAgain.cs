using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeCoRoutinesAgain : MonoBehaviour
{
    private int _value = 0;
    private Text _timerText;
    private bool _timerIsRunning = false;
    
    void Start()
    {
        _timerText = GameObject.FindWithTag("Timer").GetComponent<Text>();
        _timerIsRunning = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && _timerIsRunning == false)
        {
            _timerIsRunning = true;
            StartCoroutine(TimerOneSecond());
        }

        if (Input.GetKeyDown(KeyCode.S) && _timerIsRunning == true)
        {
            StopCoroutine(TimerOneSecond());
            _timerIsRunning = false;
        }
    }

    IEnumerator TimerOneSecond()
    {
        yield return  new WaitForSeconds(1);
        _value++;
        _timerText.text = _value.ToString();
        
        Debug.Log(_value);
    }
}
