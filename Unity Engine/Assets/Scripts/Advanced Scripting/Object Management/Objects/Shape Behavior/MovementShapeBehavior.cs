using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementShapeBehavior : ShapeBehavior
{ 
    public Vector3 Velocity {get; set;}

    #region Physics
        public override void GameUpdate (vShape shape)
        {
            shape.transform.localPosition += Velocity * Time.deltaTime;
        }
        #endregion

    #region Create/Save/Load
        public override void Save (GameDataWriter writer)
        {
            writer.Write(Velocity);
        }
        public override void Load (GameDataReader reader)
        {
            Velocity = reader.ReadVector3();
        }
        #endregion
}
