using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize = 10, ySize = 5;
        private Vector3[] _vertices;

    private Mesh _mesh; 
    
    void Awake()
    {
        StartCoroutine(Generate());
    }
    
    #region Generation
        private IEnumerator Generate()
        {
            WaitForSeconds wait = new WaitForSeconds(0.05f);

            //Vertex & UV
                _vertices = new Vector3[(xSize + 1) * (ySize + 1)];
                Vector2[] uv = new Vector2[_vertices.Length];
                Vector4[] tangents = new Vector4[_vertices.Length];
                    Vector4 tangent = new Vector4(1f, 0, 0, -1f);
                    
                    for (int i = 0, y = 0; y <= ySize; y++)
                    {
                        for (int x = 0; x <= xSize; x++, i++)
                        {
                            _vertices[i] = new Vector3(x, y); //Freakin' radzies (I Love This)
                            uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                            tangents[i] = tangent;
                            yield return wait;
                        }
                    }
                _mesh.vertices = _vertices;
                _mesh.uv = uv;
                _mesh.tangents = tangents;
            
            //Mesh
                GetComponent<MeshFilter>().mesh = _mesh = new Mesh(); //Assigning Simultaneously
                _mesh.name = "Procedural Grid";
                    
                int[] triangles = new int[xSize * ySize *  6]; //6 = vertices
                
                    for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) //This will form squares fully-mapped 
                    {
                        for (int x = 0; x < xSize; x++, ti += 6, vi++)
                        {
                            triangles[ti] = vi;
                            triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                            triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                            triangles[ti + 5] = vi + xSize + 2;
                            
                                _mesh.triangles = triangles; //Forward faces have clockwise vertices
                                _mesh.RecalculateNormals();
                                yield return wait;
                        }
                    }
        }
    
        private void OnDrawGizmos()
        {
            if (_vertices == null) return; //For Edit Mode Errors
            
            Gizmos.color = Color.black;
                for (int i = 0; i < _vertices.Length; i++)
                {
                    Gizmos.DrawWireSphere(_vertices[i], 0.1f);
                }
        }
        #endregion
}
