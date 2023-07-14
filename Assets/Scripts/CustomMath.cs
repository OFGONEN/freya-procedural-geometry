using UnityEngine;
using UnityEditor;

public static class CustomMath 
{
    public const float TAU = 6.28318530718f;
    
    public static Vector2 GetUnitVectorByAngle(float angleRadians)
    {
        return new Vector2(
            Mathf.Cos(angleRadians),
            Mathf.Sin(angleRadians)
        );
    }
}
