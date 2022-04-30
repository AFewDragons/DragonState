using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace AFewDragons
{
    [CustomEditor(typeof(DragonStateBase), true, isFallback = false)]
    public class DragonStateEntry : Editor
    {
        private bool defaultFoldout = false;

        private void OnEnable()
        {
            //serializedObject.FindProperty
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var name = serializedObject.FindProperty("StateName");
            if (string.IsNullOrWhiteSpace(name.stringValue))
            {
                EditorGUILayout.LabelField("Please enter a state name.", DragonStateResources.ErrorStyle);
            }
            EditorGUILayout.PropertyField(name);

            var useDefault = serializedObject.FindProperty("UseDefault");

            defaultFoldout = EditorGUILayout.Foldout(defaultFoldout, new GUIContent("Default Settings"));
            if (defaultFoldout)
            {
                EditorGUILayout.BeginVertical(DragonStateResources.BoxStyle);
                EditorGUILayout.PropertyField(useDefault);
                if (useDefault.boolValue)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Default"), GUIContent.none);
                }
                EditorGUILayout.EndVertical();
            }


            DrawPropertiesExcluding(serializedObject, new string[] { "StateName", "UseDefault", "Default", "m_Script" });

            if (EditorApplication.isPlaying)
            {
                var runningValue = DragonStateManager.Get<object>(name.stringValue, null);
                EditorGUILayout.LabelField($"Value is: {runningValue}");
            }


            serializedObject.ApplyModifiedProperties();
        }

        private string[] GetVariables(SerializedObject serializedObject)
        {
            List<string> variables = new List<string>();
            BindingFlags bindingFlags = BindingFlags.DeclaredOnly | // This flag excludes inherited variables.
                                        BindingFlags.Public |
                                        BindingFlags.NonPublic |
                                        BindingFlags.Instance |
                                        BindingFlags.Static;
            foreach (FieldInfo field in typeof(DragonStateBase).GetFields(bindingFlags))
            {
                variables.Add(field.Name);
            }
            return variables.ToArray();
        }
    }
}