using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TouchPhaseDisplay
public class LearningTouchScreens : MonoBehaviour
{
    //Touch
        private Text _phaseDisplayText;
    
        private Touch _touch;
        private float _touchEndTime;
        private float _displayTime;

    void Start()
    {
        #region Assignment
            _phaseDisplayText = 
                GameObject.Find("PhaseDisplayText").GetComponent<Text>();
                #endregion
    }
        
    void Update()
    {
        #region Input
            if (Input.touchCount > 0) //touch
            {
                _touch = Input.GetTouch(0);
                _phaseDisplayText.text = _touch.phase.ToString(); //Displays the touch phase
    
                if (_touch.phase == TouchPhase.Ended) //Touch Ends
                {
                    _touchEndTime = Time.time;
                }
            }
            else if (Time.time - _touchEndTime > _displayTime)
            {
                _phaseDisplayText.text = "";
            }
            #endregion
    }
}