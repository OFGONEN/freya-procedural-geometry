using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGenerator : MonoBehaviour
{
    [Range(0.01f, 256)]
    public float radiusInner;
    [Range(0.01f, 256)]
    public float thickness;
    [Range(3, 256)]
    public int angularSegments = 3;

    private const float TAU = 6.28318530718f;

    public float RadiusOuter => radiusInner + thickness;

    private void OnDrawGizmosSelected()
    {
        DrawWiredCircle(transform.position, transform.rotation, radiusInner, angularSegments);
        DrawWiredCircle(transform.position, transform.rotation, RadiusOuter, angularSegments);
    }

    public static void DrawWiredCircle(Vector3 position, Quaternion rotation, float radius, int detail = 32)
    {
        Vector3[] points = new Vector3[detail];


        for (int i = 0; i < detail; i++)
        {
            var t = i / (float)detail;
            float angleRadians = t * TAU;

            var point2D = new Vector2(
                Mathf.Cos(angleRadians) * radius,
                Mathf.Sin(angleRadians) * radius
            );

            points[i] = position + rotation * point2D;
        }

        for (int i = 0; i < points.Length - 1; i++)
            Gizmos.DrawLine(points[i], points[i + 1]);
        
        Gizmos.DrawLine(points[points.Length - 1], points[0]);
    }

    private void Awake()
    {
    }
}