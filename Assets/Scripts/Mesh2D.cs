using UnityEngine;

[CreateAssetMenu(menuName = "Custom Mesh2D", fileName = "mesh2D_")]
public class Mesh2D : ScriptableObject
{
    [System.Serializable]
    public class Vertex
    {
        public Vector2 points; // this should've been named point
        public Vector2 normals; // this should've been named normal
        public float u; // x cordinate for the UV       
    }

    public Vertex[] vertices;
    public int[] lineIndices;

    public float CalculateUSpan()
    {
        float totalDistance = 0;

        for (int i = 0; i < lineIndices.Length; i += 2)
        {
            Vector2 a = vertices[lineIndices[i]].points;
            Vector2 b = vertices[lineIndices[i + 1]].points;
            totalDistance += Vector2.Distance(a, b);
        }

        return totalDistance;
    }
}