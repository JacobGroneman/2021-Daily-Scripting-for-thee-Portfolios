using UnityEngine;

public class GameLevel : PersistableObject
{
    [SerializeField] 
    private SpawnZone _spawnZone;
        public Vector3 SpawnPoint
        {get {return _spawnZone.SpawnPoint;}}

    #region Singleton
        public static GameLevel Current {get; private set;}
        #endregion

    void OnEnable()
    { 
        Current = this;
    }

    #region Save/Load
        public override void Save(GameDataWriter writer)
        {
            
        }
        public override void Load(GameDataReader reader)
        {
            
        }
        #endregion
}
