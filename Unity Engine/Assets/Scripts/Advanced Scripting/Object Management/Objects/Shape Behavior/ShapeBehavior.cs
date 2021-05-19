using UnityEngine;

public abstract class ShapeBehavior : MonoBehaviour
{
    #region Physics
        public abstract void GameUpdate(vShape shape);
        #endregion
   

    #region Create/Save/Load
        public abstract void Save(GameDataWriter writer);
        public abstract void Load(GameDataReader reader);
        #endregion
}
