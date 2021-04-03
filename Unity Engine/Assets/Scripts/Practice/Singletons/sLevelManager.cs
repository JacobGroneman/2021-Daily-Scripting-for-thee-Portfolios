using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sLevelManager : sMonoSingleton<sLevelManager>
{
    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("Loading Level");
    }
        public void LoadNextScene()
        {
            Debug.Log("Next Scene Loading!");
        }
}
    