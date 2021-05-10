using UnityEngine;

public class vShape : PersistableObject
{
    private MeshRenderer _meshRenderer;
        private Color _color;
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

    #region Physics
        public Vector3 AngularVelocity {get; set;} // deg./1 sec
        public Vector3 Velocity {get; set;}
        #endregion

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void GameUpdate() //Fixed Update, but reduced overhead 😘
    {
        transform.Rotate(AngularVelocity * Time.deltaTime);
        transform.localPosition += Velocity * Time.deltaTime;
    }

    #region Materials
        public void SetMaterial(Material material, int materialID)
        {
            _meshRenderer.material = material;
            MaterialID = materialID;
        }
        public void SetColor(Color color)
        {
            this._color = color;

            if (_sharedPropertyBlock == null)
            {
                _sharedPropertyBlock = new MaterialPropertyBlock();
            }
            _sharedPropertyBlock.SetColor(_colorPropertyID, color);
            _meshRenderer.SetPropertyBlock(_sharedPropertyBlock); //Research
        }
        #endregion
    
    #region Save/Load
        public override void Save(GameDataWriter writer)
        {
            base.Save(writer);
            writer.Write(_color);
            writer.Write(AngularVelocity);
            writer.Write(Velocity);
        }
        public override void Load(GameDataReader reader)
        {
            base.Load(reader);
            SetColor(reader.Version > 0 ? reader.ReadColor() : Color.white);
            AngularVelocity =
                reader.Version >= 4 ? reader.ReadVector3() : Vector3.zero;
            Velocity =
                reader.Version >= 4 ? reader.ReadVector3() : Vector3.zero;
        }
        #endregion
}
