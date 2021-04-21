using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using File = UnityEngine.Windows.File;

public class Game : MonoBehaviour
{
    public Transform Prefab;
    private List<Transform> _objects;

    public KeyCode //This is nice
        CreateKey = KeyCode.C,
        NewGameKey = KeyCode.N,
        SaveKey = KeyCode.S,
        LoadKey = KeyCode.L;

    private string _savePath;

    void Awake()
    {
        #region Initialize
            _objects = new List<Transform>();
            _savePath = Path.Combine
                (Application.persistentDataPath, "saveFile");
                #endregion
    }
    
    void Update()
    {
        if (Input.GetKeyDown(CreateKey))
        {
            CreateObject();
        }
        else if(Input.GetKey(NewGameKey))
        {
            BeginNewGame();
        }
        else if(Input.GetKeyDown(SaveKey))
        {
            Save();
        }
        else if(Input.GetKeyDown(LoadKey))
        {
            Load();
        }
    }

    #region Instantiate
        private void CreateObject()
        {
            Transform t = Instantiate(Prefab);
                t.localPosition = Random.insideUnitSphere * 5f;
                t.localRotation = Random.rotation;
                t.localScale = Vector3.one * Random.Range(0.1f, 1f);
                
                _objects.Add(t);
        }
        #endregion
    
    #region Scene
        private void BeginNewGame()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                Destroy(_objects[i].gameObject);
            }
            _objects.Clear();
        }
        private void Save()
        {
            using (var writer = new BinaryWriter //O.o Wow.
                (System.IO.File.Open(_savePath, FileMode.Create)))
            {
                writer.Write(_objects.Count);

                for (int i = 0; i < _objects.Count; i++)
                {
                    Transform t = _objects[i];
                        writer.Write(t.localPosition.x);
                        writer.Write(t.localPosition.y);
                        writer.Write(t.localPosition.z);
                }
            }
        }
        private void Load()
        {
            BeginNewGame();
            
            using (var reader = new BinaryReader //O.o Wow.
                (System.IO.File.Open(_savePath, FileMode.Open)))
            {
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    Vector3 p;
                        p.x = reader.ReadSingle();
                        p.y = reader.ReadSingle();
                        p.z = reader.ReadSingle();

                    Transform t = Instantiate(Prefab);
                        t.localPosition = p;
                        _objects.Add(t);
                }
            }
        }
        #endregion
}
