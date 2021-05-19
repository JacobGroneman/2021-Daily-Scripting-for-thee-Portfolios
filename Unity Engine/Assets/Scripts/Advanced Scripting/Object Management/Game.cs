using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using File = UnityEngine.Windows.File;

public class Game : PersistableObject
{
    #region Version/Storage
        public PersistentStorage Storage;
            private const int SaveVersion = 5; //(Multi-Shapes, Colors)
        private Random.State _mainRandomState;
        #endregion

    #region Level
        public int LevelCount;
        private int _loadedLevelBuildIndex;
        [SerializeField] private bool _reseedOnLoad;
        #endregion
        
    #region Objects
        [SerializeField]
        private vShapeFactory[] _shapeFactories;
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

    #region User Interface
        [SerializeField]
        private Slider _creationSpeedSlider;
        [SerializeField] 
        private Slider _destructionSpeedSlider;
        #endregion
            
    void Start()
    {
        #region Initialize
            _mainRandomState = Random.state;
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
            #endregion
        #region Execution
            BeginNewGame();
            StartCoroutine(LoadLevel(1));
            #endregion
    }

    void OnEnable()
    {
        #region Factory Index Assignment
            if (_shapeFactories[0].FactoryID != 0) //Load Assignment Checker
            {
                for (int i = 0; i < _shapeFactories.Length; i++)
                {
                    _shapeFactories[i].FactoryID = i; //Nice
                }    
            }
            #endregion
    }
    
    void Update()
    {
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
                StartCoroutine(LoadLevel(_loadedLevelBuildIndex));
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

    void FixedUpdate()
    {
        #region Optimization
            for (int i = 0; i < _shapes.Count; i++)
            {
                _shapes[i].GameUpdate();
            }
            #endregion

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
    }

    #region Instantiate/Destroy
        private void CreateShape()
        {
            _shapes.Add(GameLevel.Current.SpawnShape());
        }
        private void DestroyShape()
        {
            if (_shapes.Count > 0)
            {
                int index = Random.Range(0, _shapes.Count);
                    _shapes[index].Recycle();
                
                int lastIndex = _shapes.Count - 1;
                    _shapes[index] = _shapes[lastIndex];
                    _shapes.RemoveAt(lastIndex);
            }
        }
        #endregion
    
    #region Create/Save/Load
        private void BeginNewGame()
        {
            //Generate Seed
                Random.state = _mainRandomState;
                int seed = Random.Range(0, int.MaxValue) ^ (int)Time.timeScale; //Research ^ op.
                _mainRandomState = Random.state;
                    Random.InitState(seed);
                    
            _creationSpeedSlider.value = CreationSpeed = 0;
            _destructionSpeedSlider.value = DestructionSpeed = 0;
            
            for (int i = 0; i < _shapes.Count; i++)
            {
                _shapes[i].Recycle();
            }
            _shapes.Clear();
        }
        public override void Save(GameDataWriter writer)
        {
            writer.Write(_shapes.Count);
            writer.Write(Random.state);
            writer.Write(CreationSpeed);
                writer.Write(_creationProgress);
            writer.Write(DestructionSpeed);
                writer.Write(_destructionProgress);
            writer.Write(_loadedLevelBuildIndex);
            GameLevel.Current.Save(writer);
                for (int i = 0; i < _shapes.Count; i++) //Shape Properties
                {
                    writer.Write(_shapes[i].OriginFactory.FactoryID);
                    writer.Write(_shapes[i].ShapeID);
                    writer.Write(_shapes[i].MaterialID);
                    
                    _shapes[i].Save(writer);
                }
        }

        public override void Load(GameDataReader reader)
        {
            int version = -reader.ReadInt(); //Flips +- again

            if (version > SaveVersion)
            {
                Debug.LogError("Unsupported (future)save version" + version);
                return;
            }

            StartCoroutine(LoadGame(reader));
        }
            IEnumerator LoadGame(GameDataReader reader) 
            {
                int version = reader.Version;
                int count = version <= 0 ? -version : reader.ReadInt(); //Genius (see below code)
                    if (version >= 3)
                    {
                        Random.State state = reader.ReadRandomState(); 
                            if (!_reseedOnLoad) 
                            { 
                                Random.state = state; 
                            }
                        _creationSpeedSlider.value = CreationSpeed = reader.ReadFloat();
                            _creationProgress = reader.ReadFloat();
                        _destructionSpeedSlider.value = DestructionSpeed = reader.ReadFloat();
                            _destructionProgress = reader.ReadFloat();
                    }
                yield return LoadLevel(version < 2 ? 1 : reader.ReadInt());
                    if (version >= 3) 
                    { 
                        GameLevel.Current.Load(reader); 
                    }
                for (int i = 0; i < count; i++)
                {
                    int factoryID = version >= 5 ? reader.ReadInt() : 0;
                    int shapeID = version > 0 ? reader.ReadInt() : 0; 
                    int materialID = version > 0 ? reader.ReadInt() : 0;
                    
                    vShape instance = _shapeFactories[factoryID].Get(shapeID, materialID);
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
