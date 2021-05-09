using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class vShapeFactory : ScriptableObject
{
    [SerializeField] 
    private vShape[] _prefabs;

    [SerializeField] 
    private Material[] _materials;

    [SerializeField]
    private bool _recycle;
        private List<vShape>[] _pools;
    
    #region Scene
        private Scene _poolScene;
        #endregion

    #region Retrieve
        public vShape Get(int shapeID = 0, int materialID = 0)
        {
            vShape instance;
                if (_recycle)
                {
                    if (_pools == null){CreatePools();}
                    
                    List<vShape> pool = _pools[shapeID];
                    int lastIndex = pool.Count - 1;
                        if (lastIndex >=0)
                        {
                            instance = pool[lastIndex];
                            instance.gameObject.SetActive(true);
                            pool.RemoveAt(lastIndex);
                        }
                        else
                        {
                            instance = Instantiate(_prefabs[shapeID]);
                            instance.ShapeID = shapeID;
                                SceneManager.MoveGameObjectToScene
                                    (instance.gameObject, _poolScene);
                        }
                }
                else 
                {
                    instance = Instantiate
                        (_prefabs[shapeID]);
                    instance.ShapeID = shapeID;
                }
                instance.SetMaterial(_materials[materialID], materialID);
                
            return instance;
        }
        public vShape GetRandom()
        {
            return Get
            (Random.Range(0, _prefabs.Length),
                Random.Range(0, _materials.Length));
        }
        #endregion

    #region Pooling
        private void CreatePools()
        {
            _pools = new List<vShape>[_prefabs.Length];
                for (int i = 0; i < _pools.Length; i++)
                {
                    _pools[i] = new List<vShape>();
                }

            if (Application.isEditor)
            {
                _poolScene = SceneManager.GetSceneByName(name);
                    if (_poolScene.isLoaded)
                    {
                        GameObject[] rootObjects = _poolScene.GetRootGameObjects();
                            for (int i = 0; i < rootObjects.Length; i++)
                            {
                                vShape pooledShape = rootObjects[i].GetComponent<vShape>();
                                    if (!pooledShape.gameObject.activeSelf)
                                    {
                                        _pools[pooledShape.ShapeID].Add(pooledShape);
                                    }
                            }
                        
                        return;
                    }
            }
            _poolScene = SceneManager.CreateScene(name); //Cool
        }
        public void Reclaim(vShape shapeToRecycle)
        {
            if (_recycle){
                if (_pools == null){CreatePools();}}
            else {Destroy(shapeToRecycle.gameObject);}
            
            _pools[shapeToRecycle.ShapeID].Add(shapeToRecycle);
            shapeToRecycle.gameObject.SetActive(false);
        }
        #endregion
    
}
