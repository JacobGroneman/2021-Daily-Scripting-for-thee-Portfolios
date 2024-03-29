﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScreen : MonoBehaviour
{
   public int SceneToLoad;

   private void OnGUI()
   {
      GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 80, 100, 30),
         "Current Scene: " + (Application.loadedLevel + 1));

      if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 50,100, 40), 
         "Load Scene " + (SceneToLoad + 1)))
      {
         Application.LoadLevel(SceneToLoad);
         
      }
   }
}



/* Here's the bad way:
 
 (Store)
 PlayerPrefs.SetInt("Health", 100);
 
 (Recall)
 int x = PlayerPrefs.GetInt("Health");
 
 */