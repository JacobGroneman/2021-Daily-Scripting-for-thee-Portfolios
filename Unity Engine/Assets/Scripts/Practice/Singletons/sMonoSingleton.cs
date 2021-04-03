using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class sMonoSingleton<T> : MonoBehaviour where T : sMonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log(typeof(T).ToString() + " is NULL!");
            }

            return _instance;
        }
    }
        private void Awake()
        {
            _instance = this as T;
            Initialize();
        }
        
            public virtual void Initialize()
            {}
        
}
