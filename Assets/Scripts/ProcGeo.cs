using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGeo : MonoBehaviour
{
    private void Awake()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Quad";

        List<Vector3> points = new List<Vector3>()
        {
            new Vector3(-1,1,0),
            new Vector3(1,1,0),
            new Vector3(-1,-1,0),
            new Vector3(1,-1,0)
        };

        int[] triIndices = new int[]
        {
            1,0,2,
            3,1,2
        };

        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };
        
        mesh.SetVertices(points);
        mesh.SetNormals(normals);
        mesh.triangles = triIndices;
        
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
