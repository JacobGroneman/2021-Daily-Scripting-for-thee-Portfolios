using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSwitch : MonoBehaviour
{
    private GameMode _gameMode;
        private enum GameMode
            {
                Active,
                Paused,
                GameOver
            }
    private float _timeScale;
    private bool
        _isMobile,
        _isSpawning;

    void Update()
    {
        #region Game Constants
            Time.timeScale = _timeScale;    
            GameModeSettings();
            #endregion
            
        #region #Input
        //- - GameMode Toggles - -
        //Pause Game
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_gameMode == GameMode.Active)
                {
                    _gameMode = GameMode.Paused;
                }
                else if (_gameMode == GameMode.Paused)
                {
                    _gameMode = GameMode.Active;
                }
            }
        //Game Over Manual
            if (Input.GetKey(KeyCode.D) &&
                Input.GetKey(KeyCode.I) &&
                Input.GetKey(KeyCode.E) && (_gameMode == GameMode.Active))
            {
                _gameMode = GameMode.GameOver;
            }
            #endregion
    }

    #region Save/Load/ToggleMenus
        void GameModeSettings() 
        {
            switch (_gameMode)
            {
                case GameMode.Active:
                    _isMobile = true;
                    _isSpawning = true;
                    _timeScale = 1;
                    break;
                case GameMode.Paused:
                    _isMobile = false;
                    _isSpawning = false;
                    _timeScale = 0;
                    break;
                case GameMode.GameOver:
                    _isMobile = false;
                    _isSpawning = false;
                    _timeScale = 1;
                    break;
            }
        }
        #endregion
}
