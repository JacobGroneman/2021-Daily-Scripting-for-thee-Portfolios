using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sorry about the rare crappy code below, it was a really busy day!!
//But I got something in. Whoo!
public class PracticeSingletons : MonoBehaviour
{
   private static PracticeSingletons _instance;

   /*public static PracticeSingletons Instance;
   {
      get
      {
         if (_instance == null)
         {
            _instance = FindObjectOfType<PracticeSingletons>();
            if (_instance == null)
            {
               _instance = new GameObject().AddComponent<PracticeSingletons>();
            }
            
         }
      }
      return _instance;
   }*/

   private void Awake()
   {
      if (_instance != null)
      {
         Destroy(this);
      }
      
      DontDestroyOnLoad(this); //Just for testing ;)
   }
}  
