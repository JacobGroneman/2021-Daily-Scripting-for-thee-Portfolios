using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using File = UnityEngine.Windows.File;

public class Game : PersistableObject
{
    public PersistentStorage Storage;
        private const int SaveVersion = 1;

    public vShapeFactory ShapeFactory;
        private List<vShape> _shapes;
        public float CreationSpeed {get; set;}
        private float _creationProgress;

        public KeyCode //This is nice
        CreateKey = KeyCode.C,
        DestroyKey = KeyCode.X,
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
        #region Instantiation
            _creationProgress += Time.deltaTime * CreationSpeed;
                while (_creationProgress >= 1f)
                {
                    _creationProgress -= 1f;
                    CreateShape();
                }
                #endregion
        #region Input
            if (Input.GetKeyDown(CreateKey))
            {
                CreateShape();
            }
            else if (Input.GetKeyDown(DestroyKey))
            {
                DestroyShape();
            }
            else if(Input.GetKey(NewGameKey))
            {
                BeginNewGame();
            }
            else if(Input.GetKeyDown(SaveKey))
            {
                Storage.Save(this, SaveVersion);
            }
            else if(Input.GetKeyDown(LoadKey))
            {
                Storage.Load(this); //YOOO
            }
            #endregion
    }

    #region Instantiate/Destroy
        private void CreateShape()
        {
            vShape instance = ShapeFactory.GetRandom();
            
                Transform t = instance.transform;
                    t.localPosition = Random.insideUnitSphere * 5f;
                    t.localRotation = Random.rotation;
                    t.localScale = Vector3.one * Random.Range(0.1f, 1f);
                
                instance.SetColor(Random.ColorHSV
                    (hueMin:0, hueMax:1f, saturationMin: 0.5f, saturationMax: 1f,
                    valueMin: 0.25f, valueMax: 1f, alphaMin: 1f, alphaMax:1f));
                
                _shapes.Add(instance);
        }
        private void DestroyShape()
        {
            if (_shapes.Count > 0)
            {
                int index = Random.Range(0, _shapes.Count);
                    Destroy(_shapes[index].gameObject);
                
                int lastIndex = _shapes.Count - 1;
                    _shapes[index] = _shapes[lastIndex];
                    _shapes.RemoveAt(lastIndex);
            }
        }
        #endregion
    
    #region Scene
        private void BeginNewGame()
        {
            for (int i = 0; i < _shapes.Count; i++)
            {
                Destroy(_shapes[i].gameObject);
            }
            _shapes.Clear();
        }
        public override void Save(GameDataWriter writer)
        {
            writer.Write(_shapes.Count);
                for (int i = 0; i < _shapes.Count; i++)
                {
                    writer.Write(_shapes[i].ShapeID);
                    writer.Write(_shapes[i].MaterialID);
                    
                    _shapes[i].Save(writer);
                }
        }
        public override void Load(GameDataReader reader)
        {
            int version = -reader.ReadInt();//Flips +- again
                if (version > SaveVersion)
                {
                    Debug.LogError("Unsupported (future)save version" + version);
                    return;
                }
            int count = version <= 0 ? -version : reader.ReadInt(); //Genius (see below code)
                for (int i = 0; i < count; i++)
                {
                    int shapeID = version > 0 ? reader.ReadInt() : 0;
                    int materialID = version > 0 ? reader.ReadInt() : 0;
                    
                    vShape instance = ShapeFactory.Get(shapeID, materialID);
                        instance.Load(reader);
                        _shapes.Add(instance);
                }
        }
        #endregion
}

/* *int version = -reader.ReadInt();
	int count;
		if (version <= 0) 
		{
			count = -version;
		}
		else 
		{
			count = reader.ReadInt();
		} */

/*ternary operations are rad!*/
