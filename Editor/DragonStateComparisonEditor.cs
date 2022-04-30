using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

namespace AFewDragons
{
    /// <summary>
    /// Draws the editor for DragonStateComparison
    /// </summary>
    [CustomPropertyDrawer(typeof(DragonStateComparison))]
    public class DragonStateComparisonEditor : PropertyDrawer
    {
        private class EditorData
        {
            public ReorderableList ReorderableList;
            public DragonStateComparisonBase SelectedComparison;
        }

        private Editor comparisonEditor = null;

        private Dictionary<string, EditorData> cache = new Dictionary<string, EditorData>();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var cacheKey = property.propertyPath;
            EditorData data = cache[cacheKey];
            if (data == null) return 0;
            var height = EditorGUIUtility.singleLineHeight;

            return (data.ReorderableList?.GetHeight() ?? 0) + (data.SelectedComparison == null ? 0 : height + EditorGUIUtility.standardVerticalSpacing);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var cacheKey = property.propertyPath;
            EditorData data = cache[cacheKey];
            if (data == null)
            {
                data = new EditorData();
            }

            EditorGUI.BeginProperty(position, label, property);
            if (data.ReorderableList == null)
            {
                SetupReorderableList(data, property);
            }

            var height = EditorGUIUtility.singleLineHeight;

            var listRect = new Rect(position.x, position.y, position.width, data.ReorderableList.GetHeight());

            data.ReorderableList.DoList(listRect);

            DrawComparison(data, new Rect(position.x, position.y + EditorGUIUtility.standardVerticalSpacing + data.ReorderableList.GetHeight(), position.width, height));
            EditorGUI.EndProperty();
        }

        

        private void SetupReorderableList(EditorData data, SerializedProperty comparisonProperty)
        {
            Debug.Log(comparisonProperty.name);
            var comparisonListProperty = comparisonProperty.FindPropertyRelative("comparisonList");
            if (comparisonListProperty == null)
            {
                return;
            }
            data.ReorderableList = new ReorderableList(comparisonProperty.serializedObject, comparisonListProperty, true, true, true, true);
            data.ReorderableList.drawHeaderCallback = OnDrawHeader;
            data.ReorderableList.drawElementCallback = (rect, index, isActive, isFocused) => { OnDrawElement(data, rect, index, isActive, isFocused); };
            data.ReorderableList.onSelectCallback = (list) => { OnSelectElement(data, list); };
            data.ReorderableList.onRemoveCallback = (list) => { OnRemoveElement(data, list); };
            data.ReorderableList.onAddDropdownCallback = (rect, list) => { OnAddDropdown(data, rect, list); };
        }

        private void OnDrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Comparisons");
        }

        private void OnDrawElement(EditorData data, Rect rect, int index, bool isActive, bool isFocused)
        {
            if (!(0 <= index && index < data.ReorderableList.serializedProperty.arraySize)) return;
            var element = data.ReorderableList.serializedProperty.GetArrayElementAtIndex(index);
            if (element == null) return;
            var condition = element.objectReferenceValue as DragonStateComparisonBase;
            if (condition == null) return;
            EditorGUI.LabelField(rect, condition.GetEditorName());
        }

        private void OnSelectElement(EditorData data, ReorderableList list)
        {
            var isIndexValid = (0 <= list.index && list.index < list.count);
            data.SelectedComparison = isIndexValid ? (list.serializedProperty.GetArrayElementAtIndex(list.index).objectReferenceValue as DragonStateComparisonBase) : null;
            comparisonEditor = null;
        }

        private void OnRemoveElement(EditorData data, ReorderableList list)
        {
            var isIndexValid = (0 <= list.index && list.index < list.count);
            if (!isIndexValid) return;
            var comparison = list.serializedProperty.GetArrayElementAtIndex(list.index).objectReferenceValue as DragonStateComparisonBase;
            var element = list.serializedProperty.GetArrayElementAtIndex(list.index);
            if (element.propertyType == SerializedPropertyType.ObjectReference)
            {
                element.objectReferenceValue = null;
            }
            ReorderableList.defaultBehaviours.DoRemoveButton(list);
            list.serializedProperty.serializedObject.ApplyModifiedProperties();

            DragonStateUtility.RemoveFromAsset(data.ReorderableList.serializedProperty.serializedObject.targetObject);
            UnityEngine.Object.DestroyImmediate(comparison);
            OnSelectElement(data, list);
        }

        private void OnAddDropdown(EditorData data, Rect buttonRect, ReorderableList list)
        {
            var subtypes = DragonStateUtility.GetSubtypes<DragonStateComparisonBase>();
            subtypes.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            var menu = new GenericMenu();
            for (int i = 0; i < subtypes.Count; i++)
            {
                var subtype = subtypes[i];
                menu.AddItem(new GUIContent(ObjectNames.NicifyVariableName(subtype.Name).Replace("Quest Condition", string.Empty)), false, (d) => { OnAddComparison(data, d); }, subtype);
            }
            menu.ShowAsContext();
        }

        private void OnAddComparison(EditorData editorData, object data)
        {
            var type = data as Type;
            var comparison = DragonStateUtility.CreateScriptableObject(type);
            comparison.name = type.Name;
            editorData.SelectedComparison = comparison as DragonStateComparisonBase;

            DragonStateUtility.AddToAsset(editorData.SelectedComparison, editorData.ReorderableList.serializedProperty.serializedObject.targetObject);

            try
            {
                editorData.ReorderableList.serializedProperty.arraySize++;
                var lastIndex = editorData.ReorderableList.serializedProperty.arraySize - 1;
                editorData.ReorderableList.index = lastIndex;
                editorData.ReorderableList.serializedProperty.GetArrayElementAtIndex(lastIndex).objectReferenceValue = comparison;
                editorData.ReorderableList.serializedProperty.serializedObject.ApplyModifiedProperties();

            } catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError("Could not add serialized object. Exception: " + e.Message);
#endif
            }

        }
        
        private void DrawComparison(EditorData data, Rect position)
        {
            if (data.SelectedComparison == null) return;
            if (comparisonEditor == null) comparisonEditor = Editor.CreateEditor(data.SelectedComparison);

            if (GUI.Button(position, "Edit")){
                DragonStateComparisonWindowEditor.ShowWindow(comparisonEditor);
            }
            
            //comparisonEditor.DrawDefaultInspector();
        }
    }
}