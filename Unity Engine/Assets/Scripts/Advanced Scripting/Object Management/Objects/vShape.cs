using System.Collections.Generic;
using UnityEngine;

public class vShape : PersistableObject
{
    #region Origin
        public vShapeFactory OriginFactory
        {
            get {return OriginFactory;}
            set //Pseudo-Singleton 
            {
                if (OriginFactory == null)
                {
                    OriginFactory = value;
                }
                else
                {
                    Debug.LogError("Not allowed to change origin factory.");
                }
            }
        }
            private vShapeFactory originFactory;
            #endregion

        #region Mesh Renderer
            [SerializeField] 
            private MeshRenderer[] _meshRenderers;
                private Color[] _colors;
                public int ColorCount {get {return _colors.Length;}}
                private static int _colorPropertyID = Shader.PropertyToID("_Color"); //Research time
                private static MaterialPropertyBlock _sharedPropertyBlock;
            public int ShapeID
            {
                get {return _shapeID;}
                
                set
                {
                    if (_shapeID == int.MinValue 
                        && value != int.MinValue)
                    {
                        _shapeID = value;   
                    }
                    else
                    {
                        Debug.LogError("Not Allowed to Change ShapeID");
                    }
                }
            }
                private int _shapeID = int.MinValue;
            public int MaterialID { get; private set; }
            #endregion

    #region Behavior
        private List<ShapeBehavior> _behaviorList = new List<ShapeBehavior>();
        #endregion

    void Awake()
    {
        _colors = new Color[_meshRenderers.Length];
    }

    #region Physics
        public Vector3 AngularVelocity {get; set;} // deg./1 sec
        public Vector3 Velocity {get; set;}
        #endregion

    public void GameUpdate() //Fixed Update, but reduced overhead 😘
    {
        for (int i = 0; i < _behaviorList.Count; i++)
        {
            _behaviorList[i].GameUpdate(this);
        }
    }

    #region Pooling
        public void Recycle()
        {
            for (int i = 0; i < _behaviorList.Count; i++)
            {
                Destroy(_behaviorList[i]);
            }
            _behaviorList.Clear();
            
            OriginFactory.Reclaim(this);
        }
        #endregion

    #region Materials
        public void SetMaterial(Material material, int materialID)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material = material;
            }
            
            MaterialID = materialID;
        }
        public void SetColor(Color color)
        {
            if (_sharedPropertyBlock == null)
            {
                _sharedPropertyBlock = new MaterialPropertyBlock();
            }
            
            _sharedPropertyBlock.SetColor(_colorPropertyID, color);

            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _colors[i] = color;
                _meshRenderers[i].SetPropertyBlock(_sharedPropertyBlock);   
            }
        }
            public void SetColor(Color color, int index) //Variant
            {
                if (_sharedPropertyBlock == null) 
                {
                    _sharedPropertyBlock = new MaterialPropertyBlock();
                }
            
                _sharedPropertyBlock.SetColor(_colorPropertyID, color);
                    
                _colors[index] = color;
                _meshRenderers[index].SetPropertyBlock(_sharedPropertyBlock);
            }
            #endregion

    #region Behavior
        public T AddBehavior<T>() where T : ShapeBehavior //Genius
        {
            T behavior = gameObject.AddComponent<T>();
                _behaviorList.Add(behavior);
                
            return behavior;
        }
        #endregion
    
    #region Save/Load
        public override void Save(GameDataWriter writer)
        {
            //Base
            base.Save(writer);
            
            //Mesh Renderer
            writer.Write(_colors.Length);
                for (int i = 0; i < _colors.Length; i++)
                { 
                    writer.Write(_colors[i]);
                }

            //Physics
            writer.Write(AngularVelocity);
            writer.Write(Velocity);
        }
        public override void Load(GameDataReader reader)
        {
            //Base
            base.Load(reader);
            
            //Mesh Renderer
            if (reader.Version >= 5)
            {
                LoadColors(reader);
            }
            else
            {SetColor(reader.Version > 0 ? reader.ReadColor() : Color.white);}
            
            //Physics
            AngularVelocity =
                reader.Version >= 4 ? reader.ReadVector3() : Vector3.zero;
            Velocity =
                reader.Version >= 4 ? reader.ReadVector3() : Vector3.zero;
        }
            private void LoadColors(GameDataReader reader)
            {
                int count = reader.ReadInt();
                int max = count <= _colors.Length ? count : _colors.Length;
                int i = 0; //Outside Loop for Referencing
                    for (; i < max; i++)
                    {
                        SetColor(reader.ReadColor(), i);
                    }
                    if (count > _colors.Length)
                    {
                        for (; i < count; i++)
                        {
                            reader.ReadColor();
                        }
                    }
                    else if (count < _colors.Length)
                    {
                        for (; i < count; i++)
                        {
                            SetColor(Color.white, i);
                        }
                    }
            }
            #endregion
}
