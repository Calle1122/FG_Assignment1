using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    public float radius = 2f;
    public float deformationStrength = 2f;

    public AnimationCurve fallOff = new AnimationCurve();
    
    private Mesh _mesh;
    private Vector3[] _verts, _modifiedVerts;

    private PlaneGenerator _planeGen;

    void Start()
    {
        _mesh = GetComponentInChildren<MeshFilter>().mesh;
        _verts = _mesh.vertices;
        _modifiedVerts = _mesh.vertices;
        _planeGen = GetComponentInChildren<PlaneGenerator>();
    }

    public void DeformMesh(Vector3 hitPos)
    {
        for (int i = 0; i < _modifiedVerts.Length; i++)
        {
            Vector3 hitDis = (_modifiedVerts[i] - hitPos) / radius;
            
            if (hitDis.sqrMagnitude < 1f)
            {
                float falloff = fallOff.Evaluate(hitDis.magnitude);
                _modifiedVerts[i] += (Vector3.down * falloff);

                if (_modifiedVerts[i].y < _planeGen._minHeight)
                {
                    _modifiedVerts[i].y = _planeGen._minHeight;
                }
                
                //Will be used if I decide to implement adding to the terrain.
                if (_modifiedVerts[i].y > _planeGen._maxHeight)
                {
                    _modifiedVerts[i].y = _planeGen._maxHeight;
                }
            }

            //_modifiedVerts[i] = _modifiedVerts[i] - Vector3.down;
        }
        
        _planeGen._colors = new Color[_verts.Length];
        
        for (int i = 0, z = 0; z <= _planeGen.planeSizeZ; z++)
        {
            for (int x = 0; x <= _planeGen.planeSizeX; x++)
            {
                float vertHeight = Mathf.InverseLerp(_planeGen._minHeight, _planeGen._maxHeight, _modifiedVerts[i].y);

                _planeGen._colors[i] = _planeGen.grad.Evaluate(vertHeight);
                i++;
            }
        }
        
        RecalculateMesh();
    }

    void RecalculateMesh()
    {
        _mesh.vertices = _modifiedVerts;
        _mesh.colors = _planeGen._colors;
        _mesh.UploadMeshData(false);
        GetComponentInChildren<MeshCollider>().sharedMesh = _mesh;
        _mesh.RecalculateNormals();
    }
}
