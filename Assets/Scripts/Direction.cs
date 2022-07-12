using System;
using UnityEngine;

public class Direction : MonoBehaviour
{
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = UnityEditor.Handles.zAxisColor;
        UnityEditor.Handles.ArrowHandleCap(
            0,
            transform.position,
            transform.rotation * Quaternion.LookRotation(Vector3.forward),
            1f,
            EventType.Repaint
        );
#endif
    }
}