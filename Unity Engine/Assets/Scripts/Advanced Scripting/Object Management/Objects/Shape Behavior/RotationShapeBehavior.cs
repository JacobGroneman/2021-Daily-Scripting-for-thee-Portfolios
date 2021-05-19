using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationShapeBehavior : ShapeBehavior
{ 
    public Vector3 AngularVelocity {get; set;}

    #region Physics
        public override void GameUpdate (vShape shape)
        {
            shape.transform.Rotate(AngularVelocity * Time.deltaTime);
        }
        #endregion

    #region Create/Save/Load
        public override void Save (GameDataWriter writer)
        {
            writer.Write(AngularVelocity);
        }
        public override void Load (GameDataReader reader)
        {
            AngularVelocity = reader.ReadVector3();
        }
        #endregion
}
