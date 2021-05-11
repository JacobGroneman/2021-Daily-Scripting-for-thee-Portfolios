using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnZone : PersistableObject
{ 
    public abstract Vector3 SpawnPoint {get;}

    #region Configuration
       public void ConfigureSpawn(vShape shape)
        {
            Transform t = shape.transform;
            t.localPosition = SpawnPoint;
            t.localRotation = Random.rotation;
            t.localScale = Vector3.one * Random.Range(0.1f, 1f);
                
            shape.SetColor(Random.ColorHSV
            (hueMin:0, hueMax:1f, saturationMin: 0.5f, saturationMax: 1f,
                valueMin: 0.25f, valueMax: 1f, alphaMin: 1f, alphaMax:1f));

            shape.AngularVelocity = 
                Random.onUnitSphere * Random.Range(0f, 50f); //ran range/1 sec
            shape.Velocity =
                transform.forward * Random.Range(0f, 2f);
        }
       #endregion
}
    