using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] 
    private SpawnZone _spawnZone;

    #region Singleton
        public static GameLevel Current {get; private set;}
        #endregion

    void OnEnable()
    {
        Current = this;
    }
    
    void Start()
    {
        #region Initialize
            Game.Instance.SpawnZoneOfLevel = _spawnZone;
            #endregion
    }
}
