using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnZone : PersistableObject
{ 
    public abstract Vector3 SpawnPoint {get;}
    
    [System.Serializable]
    public struct SpawnConfiguration
    {
        public vShapeFactory[] Factories;
        public enum MovementDirection
        {Forward, Upward, Outward, Random}
            public MovementDirection movementDirection;
            
        public FloatRange AngularSpeed;
        public FloatRange Speed;

        public FloatRange Scale;

        public ColorRangeHSV Color;
            public bool IsUniformColor;
    }
        [SerializeField] 
        private SpawnConfiguration _spawnConfig;

    #region Configuration
        public virtual vShape SpawnShape()
        {
            int factoryIndex = Random.Range(0, _spawnConfig.Factories.Length);
            vShape shape = _spawnConfig.Factories[factoryIndex].GetRandom();
            
            Transform t = shape.transform;
                t.localPosition = SpawnPoint;
                t.localRotation = Random.rotation;
                t.localScale = Vector3.one * _spawnConfig.Scale.RandomValueInRange;

            //Color
            if (_spawnConfig.IsUniformColor)
            {
                shape.SetColor(_spawnConfig.Color.RandomInRange);
            }
            else
            {
                for (int i = 0; i < shape.ColorCount; i++)
                {
                    shape.SetColor(_spawnConfig.Color.RandomInRange, i);
                }
            }

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

            return shape;
        }
        #endregion
}
    