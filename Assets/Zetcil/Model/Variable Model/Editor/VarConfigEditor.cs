using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(VarConfig)), CanEditMultipleObjects]
    public class VarConfigEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           OperationType
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            OperationType = serializedObject.FindProperty("OperationType");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(OperationType, true);
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Initialize)
                {
                    EditorGUILayout.HelpBox("Save Mode: ON", MessageType.Warning);
                }
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Runtime)
                {
                    EditorGUILayout.HelpBox("Publish Mode: ON", MessageType.Info);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}