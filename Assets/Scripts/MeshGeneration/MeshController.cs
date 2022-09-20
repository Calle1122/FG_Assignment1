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
    
    void Start()
    {
        _mesh = GetComponentInChildren<MeshFilter>().mesh;
        _verts = _mesh.vertices;
        _modifiedVerts = _mesh.vertices;
        Debug.Log(_modifiedVerts);
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
            }

            //_modifiedVerts[i] = _modifiedVerts[i] - Vector3.down;
        }
        
        RecalculateMesh();
    }

    void Update()
    {
        
    }
    
    void RecalculateMesh()
    {
        _mesh.vertices = _modifiedVerts;
        _mesh.UploadMeshData(false);
        GetComponentInChildren<MeshCollider>().sharedMesh = _mesh;
        _mesh.RecalculateNormals();
    }
}
