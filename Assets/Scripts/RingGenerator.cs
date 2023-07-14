using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class RingGenerator : MonoBehaviour
{
    public UVProjection uvProjection = UVProjection.AngularRadial;
    [Range(0.01f, 256)]
    public float radiusInner;
    [Range(0.01f, 256)]
    public float thickness;
    [Range(3, 256)]
    public int angularSegmentCount = 3;

    float RadiusOuter => radiusInner + thickness;
    int VertexCount => angularSegmentCount * 2;
    
// Private
    Mesh mesh;
    

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "Ring Mesh";
        
        GetComponent<MeshFilter>().sharedMesh = mesh; // We can contiune to edit the mesh after this.
    }

    private void Update() => GenerateMesh();


    void GenerateMesh()
    {
        mesh.Clear();

        int vertexCount = VertexCount;
        List<Vector3> vertices = new List<Vector3>(vertexCount);
        List<Vector3> normals = new List<Vector3>(vertexCount);
        List<Vector2> uvs = new List<Vector2>(vertexCount);

        for (int i = 0; i < angularSegmentCount + 1; i++)
        {
            var t = i / (float)angularSegmentCount;
            float angleRadians = t * CustomMath.TAU;

            Vector2 direction = CustomMath.GetUnitVectorByAngle(angleRadians);
            
            vertices.Add(direction * RadiusOuter);
            vertices.Add(direction * radiusInner);
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);

            switch(uvProjection)
            {
                case UVProjection.AngularRadial:
                    uvs.Add(new Vector2(t, 1));
                    uvs.Add(new Vector2(t, 0));
                    break;
                case UVProjection.ProjectZ:
                    uvs.Add(direction * 0.5f + Vector2.one * 0.5f);
                    uvs.Add(direction * (radiusInner / RadiusOuter) * 0.5f + Vector2.one * 0.5f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        List<int> triangleIndices = new List<int>(vertexCount * 3);

        for (int i = 0; i < angularSegmentCount; i++)
        {
            var indexRoot = i * 2;
            var indexInnerRoot = indexRoot + 1;
            var indexOuterNext = (indexRoot + 2);
            var indexInnerNext = (indexRoot + 3);
            
            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexOuterNext);
            triangleIndices.Add(indexInnerNext);
            
            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexInnerNext);
            triangleIndices.Add(indexInnerRoot);
        }
        
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
    }

    private void OnDrawGizmosSelected()
    {
        CustomGizmos.DrawWiredCircleVertical(transform.position, transform.rotation, radiusInner, angularSegmentCount);
        CustomGizmos.DrawWiredCircleVertical(transform.position, transform.rotation, RadiusOuter, angularSegmentCount);

        if (EditorApplication.isPlaying)
        {
            var mesh = GetComponent<MeshFilter>().sharedMesh;
        
            CustomGizmos.DrawMeshVertexInfo(transform.position + Vector3.left * 5f, mesh);
            CustomGizmos.DrawMeshUVInfo(transform.position + Vector3.right * 5f, mesh);            
        }
    }
}

public enum UVProjection
{
    AngularRadial,
    ProjectZ
}