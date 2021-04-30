using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //(Only one script per Unity object)
public class PersistableObject : MonoBehaviour
{
    #region Save/Load
        public virtual void Save(GameDataWriter writer)
        {
            //Transform
                writer.Write(transform.localPosition);
                writer.Write(transform.localRotation); //Thank You Polymorphism
                writer.Write(transform.localScale);
        }
        public virtual void Load(GameDataReader reader)
        {
            //Transform
                transform.localPosition = reader.ReadVector3();
                transform.localRotation = reader.ReadQuaternion();
                transform.localScale = reader.ReadVector3();
        }
        #endregion
}
