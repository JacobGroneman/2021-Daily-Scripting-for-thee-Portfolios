using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchDPad : MonoBehaviour
{
   //D-Pad
      public Text DirectionText;
      private string _direction;
      
      private Touch _touch;
      private Vector2 _touchStartPos, _touchEndPos;

      void Update()
      {
          #region Input
            //Touch
                if (Input.touchCount > 0)
                {
                    _touch = Input.GetTouch(0);
                    
                    if (_touch.phase == TouchPhase.Began)
                    {
                        _touchStartPos = _touch.position;
                    }
                    
                    else if
                        (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Ended)
                    {
                        _touchEndPos = _touch.position;

                        float x = _touchEndPos.x - _touchStartPos.x;
                        float y = _touchStartPos.y - _touchEndPos.y;

                        if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0) //(Floating Point Comparison)
                        {
                            _direction = "Tapped";
                        }
                        else if(Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            _direction = x > 0 ? "Right" : "Left"; //Research This!!
                        }
                        else
                        {
                            _direction = y > 0 ? "Up" : "Down";
                        }
                    }
                }

                DirectionText.text = _direction;
                #endregion
      }
}
