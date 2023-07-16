using UnityEngine;

[CreateAssetMenu(menuName = "Custom Mesh2D", fileName = "mesh2D_")]
public class Mesh2D : ScriptableObject
{
    [System.Serializable]
    public class Vertex
    {
        public Vector2 points;
        public Vector2 normals;
        public float u;       
    }

    public Vertex[] vertices;
    public int[] lineIndices;
}