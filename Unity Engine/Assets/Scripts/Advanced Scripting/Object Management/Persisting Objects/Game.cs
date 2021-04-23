using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using File = UnityEngine.Windows.File;

public class Game : PersistableObject
{
    public PersistentStorage Storage;

    public vShapeFactory ShapeFactory;
        private List<vShape> _shapes;

    public KeyCode //This is nice
        CreateKey = KeyCode.C,
        NewGameKey = KeyCode.N,
        SaveKey = KeyCode.S,
        LoadKey = KeyCode.L;

    void Awake()
    {
        #region Initialize
            _shapes = new List<vShape>();
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
            Storage.Save(this);
        }
        else if(Input.GetKeyDown(LoadKey))
        {
            Storage.Load(this); //YOOO
        }
    }

    #region Instantiate
        private void CreateShape()
        {
            vShape instance = ShapeFactory.GetRandom();
            
            Transform t = instance.transform;
                t.localPosition = Random.insideUnitSphere * 5f;
                t.localRotation = Random.rotation;
                t.localScale = Vector3.one * Random.Range(0.1f, 1f);
                
                _shapes.Add(instance);
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
        public override void Save(GameDataWriter writer)
        {
            writer.Write(_objects.Count);
                for (int i = 0; i < _objects.Count; i++)
                {
                    _objects[i].Save(writer);
                }
        }
        public override void Load(GameDataReader reader)
        {
            int count = reader.ReadInt();
                for (int i = 0; i < count; i++)
                {
                    vShape instance = ShapeFactory.Get(0);
                        instance.Load(reader);
                        _shapes.Add(instance);
                }
        }
        #endregion
}
