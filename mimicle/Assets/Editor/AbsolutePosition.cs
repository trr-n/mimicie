using UnityEngine;
using UnityEditor;

namespace MyGame.Customize
{
    [CustomEditor(typeof(Transform))]
    public class AbsolutePos : Editor
    {
        Transform transform = null;

        void OnEnable()
        {
            transform = target as Transform;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Vector3Field("World Rotation", transform.eulerAngles);
            EditorGUILayout.Vector3Field("World Position", transform.position);
            EditorGUILayout.Vector3Field("World Scale", transform.localScale);
        }
    }
}