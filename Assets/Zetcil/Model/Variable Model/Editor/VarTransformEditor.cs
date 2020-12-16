using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarTransform)), CanEditMultipleObjects]
    public class VarTransformEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetTransform,
           usingPosition,
           PositionX,
           PositionY,
           PositionZ,
           usingRotation,
           RotationX,
           RotationY,
           RotationZ,
           usingScale,
           ScaleX,
           ScaleY,
           ScaleZ
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetTransform = serializedObject.FindProperty("TargetTransform");
            usingPosition = serializedObject.FindProperty("usingPosition");
            PositionX = serializedObject.FindProperty("PositionX");
            PositionY = serializedObject.FindProperty("PositionY");
            PositionZ = serializedObject.FindProperty("PositionZ");
            usingRotation = serializedObject.FindProperty("usingRotation");
            RotationX = serializedObject.FindProperty("RotationX");
            RotationY = serializedObject.FindProperty("RotationY");
            RotationZ = serializedObject.FindProperty("RotationZ");
            usingScale = serializedObject.FindProperty("usingScale");
            ScaleX = serializedObject.FindProperty("ScaleX");
            ScaleY = serializedObject.FindProperty("ScaleY");
            ScaleZ = serializedObject.FindProperty("ScaleZ");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TargetTransform);
                EditorGUILayout.PropertyField(usingPosition);
                EditorGUILayout.PropertyField(PositionX);
                EditorGUILayout.PropertyField(PositionY);
                EditorGUILayout.PropertyField(PositionZ);
                EditorGUILayout.PropertyField(usingRotation);
                EditorGUILayout.PropertyField(RotationX);
                EditorGUILayout.PropertyField(RotationY);
                EditorGUILayout.PropertyField(RotationZ);
                EditorGUILayout.PropertyField(usingScale);
                EditorGUILayout.PropertyField(ScaleX);
                EditorGUILayout.PropertyField(ScaleY);
                EditorGUILayout.PropertyField(ScaleZ);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}