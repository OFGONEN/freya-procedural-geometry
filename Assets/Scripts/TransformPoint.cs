using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPoint : MonoBehaviour
{
    public Transform target;
    
    [ContextMenu("Transform To Forward")]
    public void TransformToForward()
    {
        var position = Quaternion.LookRotation(transform.forward, transform.up) * target.position;
        Debug.Log("Position:" + position);
        target.rotation = Quaternion.LookRotation(transform.forward, transform.up);
        target.position = position + transform.position;
    }
}
