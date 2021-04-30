using System.IO;
using UnityEngine;

public class GameDataWriter
{
    private BinaryWriter _writer;

    #region Constructor
        public GameDataWriter(BinaryWriter writer)
        {
            this._writer = writer;
        }
        #endregion
    
    //Writing Data
        public void Write(int value) //Polymorphism at it's finest
        {
            _writer.Write(value);
        }
        public void Write(float value)
        {
            _writer.Write(value);
        }
        public void Write(Vector3 value)
        {
            _writer.Write(value.x);
            _writer.Write(value.y);
            _writer.Write(value.z);
        }
        public void Write(Quaternion value)
        {
            _writer.Write(value.x);
            _writer.Write(value.y);
            _writer.Write(value.z);
        }

        public void Write(Color value)
        {
            _writer.Write(value.r);
            _writer.Write(value.g);
            _writer.Write(value.b);
            _writer.Write(value.a);
        }
}

public class GameDataReader
{
    public int Version {get;}
    private BinaryReader _reader;

    #region Constructor
        public GameDataReader(BinaryReader reader, int version)
        {
            this._reader = reader;
            this.Version = version;
        }
        #endregion
        
    //Reading Data
        public int ReadInt()
        {
            return _reader.ReadInt32();
        }
        public float ReadFloat()
        {
            return _reader.ReadSingle();
        }
        public Vector3 ReadVector3()
        {
            Vector3 value;
                value.x = _reader.ReadSingle();
                value.y = _reader.ReadSingle();
                value.z = _reader.ReadSingle();
            
            return value;
        }
        public Quaternion ReadQuaternion()
        {
            Quaternion value;
                value.x = _reader.ReadSingle();
                value.y = _reader.ReadSingle();
                value.z = _reader.ReadSingle();
                value.w = _reader.ReadSingle();
            
            return value;
        }
        public Color ReadColor()
        {
            Color value;
                value.r = _reader.ReadSingle();
                value.g = _reader.ReadSingle();
                value.b = _reader.ReadSingle();
                value.a = _reader.ReadSingle();
                
            return value;
        }
}
