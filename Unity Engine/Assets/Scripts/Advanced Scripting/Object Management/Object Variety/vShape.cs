using UnityEngine;

public class vShape : PersistableObject
{
    private Color _color;
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

    #region Materials
        public void SetMaterial(Material material, int materialID)
        {
            GetComponent<MeshRenderer>().material = material;
            MaterialID = materialID;
        }
        public void SetColor(Color color)
        {
            this._color = color;
            GetComponent<MeshRenderer>().material.color = color;
        }
        #endregion
    
    #region Save/Load
        public override void Save(GameDataWriter writer)
        {
            base.Save(writer);
            writer.Write(_color);
        }
        public override void Load(GameDataReader reader)
        {
            base.Load(reader);
            SetColor(reader.ReadColor());
        }
        #endregion
}
