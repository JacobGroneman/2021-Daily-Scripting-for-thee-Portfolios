using System.IO;
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
        public void Save(PersistableObject obj, int version)
        {
            using (var writer = new BinaryWriter
                (System.IO.File.Open(_savePath, FileMode.Create)))
            {
                writer.Write(-version);
                obj.Save(new GameDataWriter(writer));
            }
        }
        public void Load(PersistableObject obj)
        {
            byte[] data = File.ReadAllBytes(_savePath);
            var reader = new BinaryReader(new MemoryStream(data));
                obj.Load(new GameDataReader(reader, -reader.ReadInt32()));
        }
        #endregion
}
