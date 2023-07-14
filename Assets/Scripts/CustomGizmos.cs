using UnityEngine;
using UnityEditor;

public static class CustomGizmos 
{
    
    public static void DrawWiredCircleVertical(Vector3 position, Quaternion rotation, float radius, int detail = 32)
    {
        Vector3[] points = new Vector3[detail];


        for (int i = 0; i < detail; i++)
        {
            var t = i / (float)detail;
            float angleRadians = t * CustomMath.TAU;

            var point2D = CustomMath.GetUnitVectorByAngle(angleRadians) * radius;

            points[i] = position + rotation * point2D;
        }

        for (int i = 0; i < points.Length - 1; i++)
            Gizmos.DrawLine(points[i], points[i + 1]);
        
        Gizmos.DrawLine(points[points.Length - 1], points[0]);
    }
    
    public static void DrawWiredCircleHorizontal(Vector3 position, Quaternion rotation, float radius, int detail = 32)
    {
        Vector3[] points = new Vector3[detail];


        for (int i = 0; i < detail; i++)
        {
            var t = i / (float)detail;
            float angleRadians = t * CustomMath.TAU;

            Vector3 point = CustomMath.GetUnitVectorByAngle(angleRadians) * radius;
            point.z = point.y;
            point.y = 0;

            points[i] = position + rotation * point;
        }

        for (int i = 0; i < points.Length - 1; i++)
            Gizmos.DrawLine(points[i], points[i + 1]);
        
        Gizmos.DrawLine(points[points.Length - 1], points[0]);
    }

    public static void DrawMeshVertexInfo(Vector3 position, Mesh mesh)
    {
        var vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length - 2; i++)
        {
            var point = position + vertices[i]; 
            Handles.Label(point, $"Vertex({i}): {point}");
        }

        for (int i = vertices.Length - 2; i < vertices.Length; i++)
        {
            var point = position + vertices[i]; 
            Handles.Label(point + Vector3.up * 0.1f, $"Vertex({i}): {point}");
        }
    }
    
    public static void DrawMeshUVInfo(Vector3 position, Mesh mesh)
    {
        var vertices = mesh.vertices;
        var uvs = mesh.uv;
        for (int i = 0; i < vertices.Length - 2; i++)
        {
            var point = position + vertices[i]; 
            Handles.Label(point, $"UV({i}): {uvs[i]}");
        }

        for (int i = vertices.Length - 2; i < vertices.Length; i++)
        {
            var point = position + vertices[i]; 
            Handles.Label(point + Vector3.up * 0.1f, $"UV({i}): {uvs[i]}");
        }
    }
}