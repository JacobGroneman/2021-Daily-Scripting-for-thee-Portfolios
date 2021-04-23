﻿using System.IO;
using UnityEngine;
using File = UnityEngine.Windows.File;

public class PersistentStorage : MonoBehaviour
{
    private string _savePath;

    void Awake()
    {
        #region Initialize
            _savePath = Path.Combine
                (Application.persistentDataPath, "saveFile");
                #endregion
    }

    #region Save/Load
        public void Save(PersistableObject obj)
        {
            using (var writer = new BinaryWriter
                (System.IO.File.Open(_savePath, FileMode.Create)))
            {
                obj.Save(new GameDataWriter(writer));
            }
        }
        public void Load(PersistableObject obj)
        {
            using (var reader = new BinaryReader
                (System.IO.File.Open(_savePath, FileMode.Open)))
            {
                obj.Load(new GameDataReader(reader));
            }
        }
        #endregion
}