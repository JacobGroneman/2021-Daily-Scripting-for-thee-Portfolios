using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] 
    private SpawnZone _spawnZone;
    
    void Start()
    {
        #region Initialize
            Game.Instance.SpawnZoneOfLevel = _spawnZone;
            #endregion
    }
}
