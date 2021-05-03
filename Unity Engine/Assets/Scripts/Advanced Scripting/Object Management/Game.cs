using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using File = UnityEngine.Windows.File;

public class Game : PersistableObject
{
    #region Version/Storage
        public PersistentStorage Storage;
            private const int SaveVersion = 2;
            #endregion

    #region Level
        public int LevelCount;
        private int _loadedLevelBuildIndex;
        #endregion
        
    #region Objects
        public SpawnZone SpawnZoneOfLevel {get; set;}
        [SerializeField]
        private vShapeFactory ShapeFactory;
            private List<vShape> _shapes;
        public float CreationSpeed {get; set;}
            private float _creationProgress;
        public float DestructionSpeed {get; set;}
            private float _destructionProgress;
            #endregion

    #region Input
        public KeyCode //This is nice
            CreateKey = KeyCode.C,
            DestroyKey = KeyCode.X,
            NewGameKey = KeyCode.N, 
            SaveKey = KeyCode.S,
            LoadKey = KeyCode.L;
            #endregion

    #region Singleton
        public static Game Instance {get; private set;}
        #endregion

    void OnEnable()
    {
        Instance = this;
    }
        
    void Start()
    {
        #region Initialize
            Instance = this;
            _shapes = new List<vShape>();
            if (Application.isEditor)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene loadedScene = SceneManager.GetSceneAt(i);
                        if (loadedScene.name.Contains("Level"))
                        {
                            SceneManager.SetActiveScene(loadedScene);
                            _loadedLevelBuildIndex = loadedScene.buildIndex;
                            return;
                        }
                }
            }
            StartCoroutine(LoadLevel(1));
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
            _destructionProgress += DestructionSpeed * Time.deltaTime;
                while (_destructionProgress >= 1f)
                {
                    _destructionProgress -= 1f;
                    DestroyShape();
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
            else
            {
                for (int i = 0; i < LevelCount; i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                    {
                        BeginNewGame();
                        StartCoroutine(LoadLevel(i));
                        return;
                    }
                }
            }
            #endregion
    }

    #region Instantiate/Destroy
        private void CreateShape()
        {
            vShape instance = ShapeFactory.GetRandom();
            
                Transform t = instance.transform;
                    t.localPosition = SpawnZoneOfLevel.SpawnPoint;
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
                    ShapeFactory.Reclaim(_shapes[index]);
                
                int lastIndex = _shapes.Count - 1;
                    _shapes[index] = _shapes[lastIndex];
                    _shapes.RemoveAt(lastIndex);
            }
        }
        #endregion
    
    #region Create/Save/Load
        private void BeginNewGame()
        {
            for (int i = 0; i < _shapes.Count; i++)
            {
                ShapeFactory.Reclaim(_shapes[i]);
            }
            _shapes.Clear();
        }
        public override void Save(GameDataWriter writer)
        {
            writer.Write(_shapes.Count);
            writer.Write(_loadedLevelBuildIndex);
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
            StartCoroutine(LoadLevel(version < 2 ? 1 : reader.ReadInt()));
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
    
    #region Level
        private IEnumerator LoadLevel(int levelBuildIndex)
        {
            enabled = false; //disables Game.cs monobehavior for LoadLevel() (to prevent player control!)

            if (_loadedLevelBuildIndex > 0)
            {
                yield return SceneManager.UnloadSceneAsync
                    (_loadedLevelBuildIndex);
            }

            yield return SceneManager.LoadSceneAsync
                (levelBuildIndex, LoadSceneMode.Additive);
            
            SceneManager.SetActiveScene
                (SceneManager.GetSceneByBuildIndex(levelBuildIndex));

            _loadedLevelBuildIndex = levelBuildIndex;

            enabled = true;
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
