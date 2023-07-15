using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    [SerializeField] private Transform[] controlPoints = new Transform[4];
    [SerializeField] private float gizmoRadius;
    [SerializeField, Range(0,1)] private float tTest;
    
    Vector3 GetPosition(int index) => controlPoints[index].position;
    
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

        Gizmos.color = Color.red;

        var testOrientedPoint = GetBezierOrientedPoint(tTest);
        Gizmos.DrawSphere(testOrientedPoint.position, gizmoRadius / 2f);
        Handles.PositionHandle(testOrientedPoint.position, testOrientedPoint.rotation);
            
        Gizmos.color = Color.white;
    }
}