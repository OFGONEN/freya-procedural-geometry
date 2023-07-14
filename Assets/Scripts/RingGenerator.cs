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

    float RadiusOuter => radiusInner + thickness;
    int VertexCount => angularSegments * 2;
    

    private void OnDrawGizmosSelected()
    {
        CustomGizmos.DrawWiredCircleVertical(transform.position, transform.rotation, radiusInner, angularSegments);
        CustomGizmos.DrawWiredCircleVertical(transform.position, transform.rotation, RadiusOuter, angularSegments);
    }
}