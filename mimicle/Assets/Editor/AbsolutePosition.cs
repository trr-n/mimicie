using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class AbsolutePos : Editor
{
    Transform transform = null;

    private void OnEnable()
    {
        transform = target as Transform;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Vector3Field("World Position", transform.position);
        EditorGUILayout.Vector3Field("World Rotation", transform.eulerAngles);
    }
}