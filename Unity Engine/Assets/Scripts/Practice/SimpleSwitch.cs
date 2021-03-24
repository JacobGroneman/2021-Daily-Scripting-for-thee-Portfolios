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

    private bool
        _isMobile,
        _isSpawning;

    private float _timeScale;

    void Update()
    {
        #region #Input
        //Save/Load/ToggleMenus
            if (Input.GetKeyDown(KeyCode.Escape))
            {//Pause Game
                if (_gameMode == GameMode.Active)
                {
                    _gameMode = GameMode.Paused;
                    ApplyGameModeSettings();
                }
                else if (_gameMode == GameMode.Paused)
                {
                    _gameMode = GameMode.Active;
                    ApplyGameModeSettings();
                }
            }

            if (Input.GetKey(KeyCode.D) &&
                Input.GetKey(KeyCode.I) &&
                Input.GetKey(KeyCode.E) && (_gameMode == GameMode.Active))
            {//Game Over Manual
                _gameMode = GameMode.GameOver;
                ApplyGameModeSettings();
            }
            #endregion
            
            #region Master Properties
                Time.timeScale = _timeScale;
                #endregion
    }

    #region Save/Load/ToggleMenus
        void ApplyGameModeSettings() 
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
