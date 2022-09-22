using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaneGenerator : MonoBehaviour
{
    public int planeSizeX;
    public int planeSizeZ;

    private Mesh _mesh;

    private int[] _tris;
    private Vector3[] _verts;

    public Gradient grad;
    public Color[] _colors;

    public float _minHeight = -5f;
    public float _maxHeight = 0f;
    
    void Awake()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        GeneratePlane();
        UpdateMesh();
    }
    
    void Start()
    {
        
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

                //Slope 2 sides
                if (x == 0 || z == 0)
                {
                    _verts[i] = new Vector3(x, -5.5f, z);
                }
                if (x == 1 || z == 1)
                {
                    _verts[i] = new Vector3(x, -3.5f, z);
                }
                if (x == 2 || z == 2)
                {
                    _verts[i] = new Vector3(x, -2f, z);
                }
                if (x == 3 || z == 3)
                {
                    _verts[i] = new Vector3(x, -0.75f, z);
                }
                
                //Slope other 2 sides
                if (x == planeSizeX|| z == planeSizeZ)
                {
                    _verts[i] = new Vector3(x, -5.5f, z);
                }
                if (x == planeSizeX - 1|| z == planeSizeZ - 1)
                {
                    _verts[i] = new Vector3(x, -3.5f, z);
                }
                if (x == planeSizeX - 2|| z == planeSizeZ - 2)
                {
                    _verts[i] = new Vector3(x, -2f, z);
                }
                if (x == planeSizeX - 3|| z == planeSizeZ - 3)
                {
                    _verts[i] = new Vector3(x, -0.75f, z);
                }
                
                i++;
            }
        }

        int triCounter = 0;
        int vertCounter = 0;
        
        for (int z = 0; z < planeSizeZ; z++)
        {
            for (int x = 0; x < planeSizeX; x++)
            {
                //0-2 generates first tri in tile
                _tris[triCounter + 0] = vertCounter + 0;
                _tris[triCounter + 1] = vertCounter + planeSizeZ + 1;
                _tris[triCounter + 2] = vertCounter + 1;
                
                //3-5 generates second tri in tile
                _tris[triCounter + 3] = vertCounter + 1;
                _tris[triCounter + 4] = vertCounter + planeSizeZ + 1;
                _tris[triCounter + 5] = vertCounter + planeSizeZ + 2;

                triCounter += 6;
                vertCounter++;
            }

            vertCounter++;
        }

        _colors = new Color[_verts.Length];
        
        for (int i = 0, z = 0; z <= planeSizeZ; z++)
        {
            for (int x = 0; x <= planeSizeX; x++)
            {
                _colors[i] = grad.Evaluate(.5f);
                i++;
            }
        }
    }

    void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _verts;
        _mesh.triangles = _tris;
        _mesh.colors = _colors;
        _mesh.RecalculateNormals();

        gameObject.AddComponent<MeshCollider>().sharedMesh = _mesh;

        this.transform.tag = "Ground";
    }
}
