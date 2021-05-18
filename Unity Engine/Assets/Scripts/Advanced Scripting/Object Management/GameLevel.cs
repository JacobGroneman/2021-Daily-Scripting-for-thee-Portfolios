using UnityEngine;

public class GameLevel : PersistableObject
{
    [SerializeField] 
    private SpawnZone _spawnZone;
    [SerializeField]
    private PersistableObject[] _persistentObjects;

        #region Singleton
        public static GameLevel Current {get; private set;}
        #endregion

    void OnEnable()
    {
        #region Singleton
            Current = this;
            #endregion

        if (_persistentObjects == null)
        {
            _persistentObjects = new PersistableObject[0];
        }
    }

    #region Configure
        public vShape SpawnShape()
        {
            return _spawnZone.SpawnShape();
        }
        #endregion

    #region Save/Load
        public override void Save(GameDataWriter writer)
        {
            writer.Write(_persistentObjects.Length);
                for (int i = 0; i < _persistentObjects.Length; i++)
                {
                    _persistentObjects[i].Save(writer);
                }
        }
        public override void Load(GameDataReader reader)
        {
            int saveCount = reader.ReadInt();
                for (int i = 0; i < saveCount; i++)
                {
                    _persistentObjects[i].Load(reader);
                }
        }
        #endregion
}
