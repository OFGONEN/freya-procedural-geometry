using UnityEngine;

public struct OrientedPoint
{
    public Vector3 position;
    public Quaternion rotation;
    
    public OrientedPoint(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
    
    public OrientedPoint(Vector3 position, Vector3 forward)
    {
        this.position = position;
        this.rotation = Quaternion.LookRotation(forward);
    }
    
    public Vector3 LocalToWorldPosition(Vector3 localPoint) => position + rotation * localPoint ;
    public Vector3 LocalToWorldVector(Vector3 localPoint) => rotation * localPoint ;
}