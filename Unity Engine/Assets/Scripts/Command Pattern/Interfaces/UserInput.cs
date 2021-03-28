using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    void Update()
    {
        #region Mouse
        //Left
            if (Input.GetMouseButtonDown(0))
            {
                CursorChangeObjectColor();
            }
            #endregion
    }
    
    #region Actions
        void CursorChangeObjectColor()
        {
            Ray rayOrigin = 
                Camera.main.ScreenPointToRay(Input.mousePosition);
                
            RaycastHit hitInfo;
    
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.collider.tag == "cube")
                {
                    ICommand click = new ClickCommand(
                            hitInfo.collider.gameObject,
                            new Color(Random.value, Random.value, Random.value));
                }
            }
        }
        #endregion
}
