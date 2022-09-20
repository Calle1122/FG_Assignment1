using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaneGenerator : MonoBehaviour
{
    public int planeSizeX;
    public int planeSizeZ;

    private Mesh _mesh;

    private int[] _tris;
    private Vector3[] _verts;

    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        GeneratePlane();
        UpdateMesh();
    }

    void GeneratePlane()
    {
        // * 6 because each tri has 3 verts and I want to generate 2 tris for each tile, making it 6 verts.
        _tris = new int[planeSizeX * planeSizeZ * 6];
        // + 1 because there is always 1 more vert than there are tiles.
        _verts = new Vector3[(planeSizeX + 1) * (planeSizeZ + 1)];

        for (int i = 0, z = 0; z <= planeSizeZ; z++)
        {
            for (int x = 0; x <= planeSizeX; x++)
            {
                _verts[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        int _triCounter = 0;
        int _vertCounter = 0;
        
        for (int z = 0; z < planeSizeZ; z++)
        {
            for (int x = 0; x < planeSizeX; x++)
            {
                //0-2 generates first tri in tile
                _tris[_triCounter + 0] = _vertCounter + 0;
                _tris[_triCounter + 1] = _vertCounter + planeSizeZ + 1;
                _tris[_triCounter + 2] = _vertCounter + 1;
                
                //3-5 generates second tri in tile
                _tris[_triCounter + 3] = _vertCounter + 1;
                _tris[_triCounter + 4] = _vertCounter + planeSizeZ + 1;
                _tris[_triCounter + 5] = _vertCounter + planeSizeZ + 2;

                _triCounter += 6;
                _vertCounter++;
            }

            _vertCounter++;
        }
    }

    void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _verts;
        _mesh.triangles = _tris;
        
        _mesh.RecalculateNormals();
    }
}
