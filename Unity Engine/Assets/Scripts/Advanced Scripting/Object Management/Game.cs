using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform Prefab;
    private List<Transform> _objects;

    public KeyCode //This is nice
        CreateKey = KeyCode.C,
        NewGameKey = KeyCode.N;

    void Awake()
    {
        #region Initialize
            _objects = new List<Transform>();
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
        #endregion
    
}
