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

            GetComponent<MeshFilter>().mesh = _mesh = new Mesh(); //Assigning Simultaneously
            _mesh.name = "Procedural Grid";
            
            _vertices = new Vector3[(xSize + 1) * (ySize + 1)];
                _mesh.vertices = _vertices;
                
            int[] triangles = new int[3];
            //These will form a square
                triangles[0] = 0;
                triangles[3] = triangles[2] = 1;
                triangles[4] = triangles[1] = xSize + 1;
                triangles[5] = xSize + 2; 
            _mesh.triangles = triangles; //Forward faces have clockwise vertices
                
                for (int i = 0, y = 0; y <= ySize; y++)
                {
                    for (int x = 0; x <= xSize; x++, i++)
                    {
                        _vertices[i] = new Vector3(x, y); //Freakin' radzies (I Love This)
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
