using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AFewDragons
{
    [CustomPropertyDrawer(typeof(GlobalStateComparison))]
    public class GlobalStateComparisonEditor : PropertyDrawer
    {
        private bool foldout = false;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;

            if (foldout)
            {
                height += (5 + EditorGUIUtility.singleLineHeight);
                var type = property.FindPropertyRelative("Type");
                if ((GlobalStateComparisonType)type.enumValueIndex != GlobalStateComparisonType.None)
                {
                    height += (5 + EditorGUIUtility.singleLineHeight);
                }
            }
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            //Properties
            var stateName = property.FindPropertyRelative("StateName");
            var type = property.FindPropertyRelative("Type");

            var headerRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var typeRect = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 5), 95, EditorGUIUtility.singleLineHeight);

            foldout = EditorGUI.BeginFoldoutHeaderGroup(headerRect, foldout, label);
            if (foldout)
            {
                EditorGUI.PropertyField(typeRect, type, GUIContent.none);
                switch ((GlobalStateComparisonType)type.enumValueIndex)
                {
                    case GlobalStateComparisonType.Boolean:
                        var booleanValueRect = new Rect(position.x + 110, position.y + (EditorGUIUtility.singleLineHeight + 5), position.width - 110, EditorGUIUtility.singleLineHeight);
                        var booleanValue = property.FindPropertyRelative("BoolValue");
                        EditorGUI.PropertyField(booleanValueRect, booleanValue, GUIContent.none);
                        var booleanComparitorRect = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 5) * 2, position.width, EditorGUIUtility.singleLineHeight);
                        var booleanComparitor = property.FindPropertyRelative("BooleanComparitor");
                        EditorGUI.PropertyField(booleanComparitorRect, booleanComparitor);
                        
                        break;
                    case GlobalStateComparisonType.Int:
                        var intValueRect = new Rect(position.x + 110, position.y + (EditorGUIUtility.singleLineHeight + 5), position.width - 110, EditorGUIUtility.singleLineHeight);
                        var intValue = property.FindPropertyRelative("IntValue");
                        EditorGUI.PropertyField(intValueRect, intValue, GUIContent.none);
                        var intCompareRect = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 5) * 2, 95, EditorGUIUtility.singleLineHeight);
                        var intCompareValue = property.FindPropertyRelative("IntComparitorType");
                        EditorGUI.PropertyField(intCompareRect, intCompareValue, GUIContent.none);
                        var intComparitorRect = new Rect(position.x + 110, position.y + (EditorGUIUtility.singleLineHeight + 5) * 2, position.width-110, EditorGUIUtility.singleLineHeight);
                        var intComparitorValue = property.FindPropertyRelative("IntComparitor");
                        EditorGUI.PropertyField(intComparitorRect, intComparitorValue, GUIContent.none);
                        break;
                    case GlobalStateComparisonType.String:
                        var stringValueRect = new Rect(position.x + 110, position.y + (EditorGUIUtility.singleLineHeight + 5), position.width - 110, EditorGUIUtility.singleLineHeight);
                        var stringValue = property.FindPropertyRelative("StringValue");
                        EditorGUI.PropertyField(stringValueRect, stringValue, GUIContent.none);
                        var stringComparitorRect = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 5) * 2, position.width, EditorGUIUtility.singleLineHeight);
                        var stringComparitor = property.FindPropertyRelative("StringComparitor");
                        EditorGUI.PropertyField(stringComparitorRect, stringComparitor);
                        break;
                }
            }
            EditorGUI.EndFoldoutHeaderGroup();
            
            EditorGUI.EndProperty();
        }
    }
}