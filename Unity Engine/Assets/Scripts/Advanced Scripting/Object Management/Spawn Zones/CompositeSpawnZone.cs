﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeSpawnZone : SpawnZone
{
    [SerializeField] 
    private SpawnZone[] _spawnZones;
    [SerializeField]
    private bool _overrideConfig;
    [SerializeField] 
    private bool _isSequential;
        private int _nextSequentialIndex;
    public override Vector3 SpawnPoint
    {
        get
        {
            int index;
                if (_isSequential)
                {
                    index = _nextSequentialIndex++;
                        if (_nextSequentialIndex >= _spawnZones.Length)
                        {
                            _nextSequentialIndex = 0;
                        }
                }
                else
                {
                    index = Random.Range(0, _spawnZones.Length);
                }
                
            return _spawnZones[index].SpawnPoint;
        }
    }

    #region Configure
        public override vShape SpawnShape()
        {
            if (_overrideConfig)
            { 
                return base.SpawnShape();
            }
            else
            {
                int index;
                    if (_isSequential)
                    {
                        index = _nextSequentialIndex++;
                        if (_nextSequentialIndex >= _spawnZones.Length)
                        {
                            _nextSequentialIndex = 0;
                        }
                    }
                    else
                    {
                        index = Random.Range(0, _spawnZones.Length);
                    }

                return _spawnZones[index].SpawnShape();
            }
        }
        #endregion

    #region Save/Load
        public override void Save(GameDataWriter writer)
        {
            writer.Write(_nextSequentialIndex);
        }
        public override void Load(GameDataReader reader)
        {
            _nextSequentialIndex = reader.ReadInt();
        }
        #endregion
}
