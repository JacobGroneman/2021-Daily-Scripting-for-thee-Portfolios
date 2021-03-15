using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiTouchDisplay : MonoBehaviour
{
    private Text _multiTouchInfoDisplay;

    private int _maxTapCount = 0;
    private string _multiTouchInfo;
    private Touch _touch;

    void Start()
    {
        #region Assignment
        _multiTouchInfoDisplay = 
            GameObject.Find("MultiTouchInfoDisplay").GetComponent<Text>();
            #endregion
    }

    void Update()
    {
        #region Input
            MultiTouchInput();
            #endregion
    }

    #region Input
        void MultiTouchInput()
        {
            //This is like the C++ string format I was using for Unreal
            _multiTouchInfo = string.Format //$
                ("Max tap count: {0}\n", _maxTapCount);
                
            //Phases
            if (Input.touchCount > 0) 
            {
                for (int i = 0; i < Input.touchCount; i++)
                { 
                    _touch = Input.GetTouch(i);
        
                    _multiTouchInfo += string.Format //$
                    ("Touch {0} - Position {1} - Tap Count: {2} - Finger ID: {3}\n" +
                     "Radius: {4} - ({5:F1}%)\n",
                        /*0*/i, /*1*/_touch.position, /*2*/_touch.tapCount,
                        /*3*/_touch.fingerId, /*4*/_touch.radius, 
                        /*5*/((_touch.radius / (_touch.radius + _touch.radiusVariance)) * 100f));
        
                    if (_touch.tapCount > _maxTapCount)
                    {
                        _maxTapCount = _touch.tapCount;
                    }
                } 
            }
            _multiTouchInfoDisplay.text = _multiTouchInfo;
        }
        #endregion
}
