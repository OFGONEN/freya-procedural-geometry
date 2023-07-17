using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(MeshFilter))]
public class RoadSegment : MonoBehaviour
{
    [SerializeField] private Mesh2D shape2d;
    [FormerlySerializedAs("roadSegmentCount")] [SerializeField, Range(2, 32)] private int edgeRingCount = 8;
    [SerializeField] private Transform[] controlPoints = new Transform[4];
    [SerializeField] private float gizmoRadius;
    [SerializeField, Range(0,1)] private float tTest;

    private Mesh mesh;
    private List<Vector3> mesh_vertices;
    private List<Vector3> mesh_normals;
    private List<int> mesh_triangle_indices;
    
    Vector3 GetPosition(int index) => controlPoints[index].position;

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "road_segment";
        
        mesh_vertices = new List<Vector3>(shape2d.vertices.Length * edgeRingCount);
        mesh_normals = new List<Vector3>(shape2d.vertices.Length * edgeRingCount);
        mesh_triangle_indices = new List<int>(shape2d.vertices.Length * edgeRingCount * 3); 
        
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    private void Update() => GenerateMesh();

    void GenerateMesh()
    {
        mesh.Clear();
        
        mesh_vertices.Clear();
        mesh_normals.Clear();
        mesh_triangle_indices.Clear();

        for (int ring = 0; ring < edgeRingCount; ring++)
        {
            float t = ring / (edgeRingCount - 1f);
            OrientedPoint orientedPoint = GetBezierOrientedPoint(t);
            
            for (int i = 0; i < shape2d.vertices.Length; i++)
            {
                mesh_vertices.Add( orientedPoint.LocalToWorldPosition(shape2d.vertices[i].points));
                mesh_normals.Add( orientedPoint.LocalToWorldVector(shape2d.vertices[i].normals));
            }
        }

        for (int ring = 0; ring < edgeRingCount - 1; ring++)
        {
            int rootIndex = ring * shape2d.vertices.Length;
            int rootIndexNext = (ring + 1) * shape2d.vertices.Length;

            for (int line = 0; line < shape2d.lineIndices.Length; line += 2)
            {
                int vertexIndexCurrent = shape2d.lineIndices[line];
                int vertexIndexNext = shape2d.lineIndices[line + 1];

                int currentVertexIndexA = rootIndex + vertexIndexCurrent;
                int currentVertexIndexB = rootIndex + vertexIndexNext;
                int nextVertexIndexA = rootIndexNext + vertexIndexCurrent;
                int nextVertexIndexB = rootIndexNext + vertexIndexNext;
                
                mesh_triangle_indices.Add(currentVertexIndexA);
                mesh_triangle_indices.Add(nextVertexIndexA);
                mesh_triangle_indices.Add(nextVertexIndexB);
                
                mesh_triangle_indices.Add(currentVertexIndexA);
                mesh_triangle_indices.Add(nextVertexIndexB);
                mesh_triangle_indices.Add(currentVertexIndexB);
            }
        }
        
        mesh.SetVertices(mesh_vertices);
        mesh.SetNormals(mesh_normals);
        mesh.SetTriangles(mesh_triangle_indices, 0);
    }

    OrientedPoint GetBezierOrientedPoint(float t)
    {
        var p0 = GetPosition(0);
        var p1 = GetPosition(1);
        var p2 = GetPosition(2);
        var p3 = GetPosition(3);

        var a = Vector3.Lerp(p0, p1, t);
        var b = Vector3.Lerp(p1, p2, t);
        var c = Vector3.Lerp(p2, p3, t);
        
        var d = Vector3.Lerp(a, b, t);
        var e = Vector3.Lerp(b, c, t);

        var position = Vector3.Lerp(d, e, t);
        var tangent = (e - d).normalized;

        return new OrientedPoint(position, tangent);
    }
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            Gizmos.DrawSphere(GetPosition(i), gizmoRadius);
        }
        
        Handles.DrawBezier(
            GetPosition(0),
            GetPosition(3),
            GetPosition(1),
            GetPosition(2),
            Color.red,
            EditorGUIUtility.whiteTexture,
            1f
        );

        var orientedPoint = GetBezierOrientedPoint(tTest);
        Handles.PositionHandle(orientedPoint.position, orientedPoint.rotation);

        void DrawPoint(Vector2 localPosition) =>
            Gizmos.DrawSphere(orientedPoint.LocalToWorldPosition(localPosition), 0.15f);

        for (int i = 0; i < shape2d.lineIndices.Length - 1; i += 2)
        {
            var worldPointOne = orientedPoint.LocalToWorldPosition(shape2d.vertices[shape2d.lineIndices[i]].points);
            var worldPointTwo = orientedPoint.LocalToWorldPosition(shape2d.vertices[shape2d.lineIndices[i + 1]].points);
            
            Gizmos.DrawLine(worldPointOne, worldPointTwo);
        }
    }
}