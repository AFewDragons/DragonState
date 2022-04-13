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
        private ReorderableList reorderableList = null;
        private DragonStateComparisonBase selectedComparison = null;
        private Editor comparisonEditor = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;

            return height + (reorderableList?.GetHeight() ?? 0);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            if (reorderableList == null)
            {
                SetupReorderableList(property);
            }

            var height = EditorGUIUtility.singleLineHeight;

            var listRect = new Rect(position.x, height, position.width, reorderableList.GetHeight());

            reorderableList.DoList(listRect);

            DrawComparison(new Rect(position.x, height + reorderableList.GetHeight(), position.width, height));
            EditorGUI.EndProperty();
        }

        

        private void SetupReorderableList(SerializedProperty comparisonProperty)
        {
            Debug.Log(comparisonProperty.name);
            var comparisonListProperty = comparisonProperty.FindPropertyRelative("comparisonList");
            if (comparisonListProperty == null)
            {
                return;
            }
            reorderableList = new ReorderableList(comparisonProperty.serializedObject, comparisonListProperty, true, true, true, true);
            reorderableList.drawHeaderCallback = OnDrawHeader;
            reorderableList.drawElementCallback = OnDrawElement;
            reorderableList.onSelectCallback = OnSelectElement;
            reorderableList.onRemoveCallback = OnRemoveElement;
            reorderableList.onAddDropdownCallback = OnAddDropdown;
        }

        private void OnDrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Comparisons");
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (!(0 <= index && index < reorderableList.serializedProperty.arraySize)) return;
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            if (element == null) return;
            var condition = element.objectReferenceValue as DragonStateComparisonBase;
            if (condition == null) return;
            EditorGUI.LabelField(rect, condition.GetEditorName());
        }

        private void OnSelectElement(ReorderableList list)
        {
            var isIndexValid = (0 <= list.index && list.index < list.count);
            selectedComparison = isIndexValid ? (list.serializedProperty.GetArrayElementAtIndex(list.index).objectReferenceValue as DragonStateComparisonBase) : null;
            comparisonEditor = null;
        }

        private void OnRemoveElement(ReorderableList list)
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
            UnityEngine.Object.DestroyImmediate(comparison);
            OnSelectElement(list);
        }

        private void OnAddDropdown(Rect buttonRect, ReorderableList list)
        {
            var subtypes = DragonStateUtility.GetSubtypes<DragonStateComparisonBase>();
            subtypes.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            var menu = new GenericMenu();
            for (int i = 0; i < subtypes.Count; i++)
            {
                var subtype = subtypes[i];
                menu.AddItem(new GUIContent(ObjectNames.NicifyVariableName(subtype.Name).Replace("Quest Condition", string.Empty)), false, OnAddComparison, subtype);
            }
            menu.ShowAsContext();
        }

        private void OnAddComparison(object data)
        {
            var type = data as Type;
            var comparison = DragonStateUtility.CreateScriptableObject(type);
            comparison.name = type.Name;
            selectedComparison = comparison as DragonStateComparisonBase;

            Debug.Log($"Created comparison {comparison.name}", comparison);

            try
            {
                reorderableList.serializedProperty.arraySize++;
                var lastIndex = reorderableList.serializedProperty.arraySize - 1;
                reorderableList.index = lastIndex;
                reorderableList.serializedProperty.GetArrayElementAtIndex(lastIndex).objectReferenceValue = comparison;
                reorderableList.serializedProperty.serializedObject.ApplyModifiedProperties();

            } catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError("Could not add serialized object. Exception: " + e.Message);
#endif
            }

        }
        
        private void DrawComparison(Rect position)
        {
            if (selectedComparison == null) return;
            if (comparisonEditor == null) comparisonEditor = Editor.CreateEditor(selectedComparison);

            if (GUI.Button(position, "Edit")){
                DragonStateComparisonWindowEditor.ShowWindow(comparisonEditor);
            }
            
            //comparisonEditor.DrawDefaultInspector();
        }
    }
}