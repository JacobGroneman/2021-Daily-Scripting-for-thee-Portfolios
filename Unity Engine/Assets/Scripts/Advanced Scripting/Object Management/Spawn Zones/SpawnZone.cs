using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnZone : PersistableObject
{ 
    public abstract Vector3 SpawnPoint {get;}
    
    [System.Serializable]
    public struct SpawnConfiguration
    {
        public enum MovementDirection
        {Forward, Upward, Outward, Random}
            public MovementDirection movementDirection;

        public FloatRange AngularSpeed;
        public FloatRange Speed;

        public FloatRange Scale;

        public ColorRangeHSV Color;
    }
        [SerializeField] 
        private SpawnConfiguration _spawnConfig;

    #region Configuration
        public virtual void ConfigureSpawn(vShape shape)
        {
            Transform t = shape.transform;
                t.localPosition = SpawnPoint;
                t.localRotation = Random.rotation;
                t.localScale = Vector3.one * _spawnConfig.Scale.RandomValueInRange;

            shape.SetColor(_spawnConfig.Color.RandomInRange);

            shape.AngularVelocity =
                Random.onUnitSphere * _spawnConfig.AngularSpeed.RandomValueInRange;
    
            Vector3 direction;
                switch (_spawnConfig.movementDirection)
                {
                    case SpawnConfiguration.MovementDirection.Upward:
                        direction = transform.up;
                        break;
                    case SpawnConfiguration.MovementDirection.Outward:
                        direction = (t.localPosition - transform.position).normalized;
                        break;
                    case SpawnConfiguration.MovementDirection.Random:
                        direction = Random.onUnitSphere;
                        break;
                    default:
                        direction = transform.forward;
                        break;
                }

                shape.Velocity = direction * _spawnConfig.Speed.RandomValueInRange;
        }
        #endregion
}
    