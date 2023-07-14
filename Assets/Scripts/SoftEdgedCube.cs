using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftEdgedCube : MonoBehaviour
{
    private void Awake()
    {
        CreateSoftEdgeCube();
    }
    

    [ContextMenu("Create Soft Edged Cube")]
    public Mesh CreateSoftEdgeCube()
    {
        var mesh = new Mesh();
        mesh.name = "soft-edged-cube";
        
        List<Vector3> points = new List<Vector3>()
        {
            // Up
            new Vector3(-1,1,-1),
            new Vector3(-1,1,1),
            new Vector3(1,1,1),
            new Vector3(1,1,-1),
            // Down
            new Vector3(-1,-1,-1),
            new Vector3(-1,-1,1),
            new Vector3(1,-1,1),
            new Vector3(1,-1,-1)
        };

        int[] triIndices = new int[]
        {
            0,1,3,
            1,2,3,
            7,3,6,
            3,2,6,
            4,0,3,
            4,3,7,
            7,5,4,
            7,6,5,
            4,5,0,
            5,1,0,
            5,2,1,
            5,6,2
        };

        List<Vector3> normals = new List<Vector3>()
        {
            new Vector3(-1,1,-1).normalized,
            new Vector3(-1,1,1).normalized,
            new Vector3(1,1,1).normalized,
            new Vector3(1,1,-1).normalized,
            // Down
            new Vector3(-1,-1,-1).normalized,
            new Vector3(-1,-1,1).normalized,
            new Vector3(1,-1,1).normalized,
            new Vector3(1,-1,-1).normalized
        };
        
        mesh.SetVertices(points);
        mesh.SetNormals(normals);
        mesh.triangles = triIndices;
        
        GetComponent<MeshFilter>().sharedMesh = mesh;

        return mesh;
    }
}
