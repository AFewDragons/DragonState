using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace AFewDragons
{
    /// <summary>
    /// Draws the editor for DragonStateComparison
    /// </summary>
    [CustomPropertyDrawer(typeof(DragonStateComparison))]
    public class DragonStateComparisonEditor : PropertyDrawer
    {
        private bool foldout = false;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;

            if (foldout)
            {
                height += (5 + EditorGUIUtility.singleLineHeight)*2;
            }
            return height;
        }

        private bool GetIsNumber(DragonStateBase state)
        {
            var st = state.GetType();
            return (st == typeof(int) ||
                st == typeof(short) ||
                st == typeof(long) ||
                st == typeof(decimal) ||
                st == typeof(double) ||
                st == typeof(float) ||
                st == typeof(ushort) ||
                st == typeof(uint) ||
                st == typeof(ulong)
                );
        }

        private void ComparitorField(Rect rect, SerializedProperty property, Type type)
        {
            if (type == typeof(int) || type == typeof(short) ||
                type == typeof(uint) || type == typeof(ushort))
            {
                var innerProp = property.FindPropertyRelative("intComparitor");
                innerProp.intValue = EditorGUI.IntField(rect, innerProp.intValue);
            } else if (type == typeof(long) || type == typeof(ulong))
            {
                var innerProp = property.FindPropertyRelative("longComparitor");
                innerProp.longValue = EditorGUI.LongField(rect, innerProp.longValue);
            } else if (type == typeof(float)) {
                var innerProp = property.FindPropertyRelative("floatComparitor");
                innerProp.floatValue = EditorGUI.FloatField(rect, innerProp.floatValue);
            } else if (type == typeof(double)) {
                var innerProp = property.FindPropertyRelative("doubleComparitor");
                innerProp.doubleValue = EditorGUI.DoubleField(rect, innerProp.doubleValue);
            } else if(type == typeof(string)) {
                var innerProp = property.FindPropertyRelative("stringComparitor");
                innerProp.stringValue = EditorGUI.TextField(rect, innerProp.stringValue);
            } else if(type == typeof(bool)) {
                var innerProp = property.FindPropertyRelative("boolComparitor");
                innerProp.boolValue = EditorGUI.Toggle(rect, innerProp.boolValue);
            } else if(type.IsSubclassOf(typeof(UnityEngine.Object))) {
                var innerProp = property.FindPropertyRelative("objectComparitor");
                innerProp.objectReferenceValue = EditorGUI.ObjectField(rect, innerProp.objectReferenceValue, type, true);
            } else {
                GUI.Label(rect, "This type of global state does not have comparible values");
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            //Properties
            var stateValue = property.FindPropertyRelative("stateValue");
            var isNumber = property.FindPropertyRelative("isNumber");
            var numberComparitorType = property.FindPropertyRelative("numberComparitorType");

            var headerRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            foldout = EditorGUI.BeginFoldoutHeaderGroup(headerRect, foldout, label);
            if (foldout)
            {
                //EditorGUI.PropertyField(typeRect, type, GUIContent.none);

                var oldStateValueType = stateValue.objectReferenceValue != null ? (stateValue.objectReferenceValue as DragonStateBase).GetType() : null;

                var stateValueRect = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 5), position.width - 110, EditorGUIUtility.singleLineHeight);
                EditorGUI.ObjectField(stateValueRect, stateValue);
                if(stateValue.objectReferenceValue != null)
                {
                    var newStateValueType = stateValue != null ? (stateValue.objectReferenceValue as DragonStateBase).GetType() : null;
                    if (oldStateValueType != newStateValueType)
                    {
                        isNumber.boolValue = GetIsNumber(stateValue.objectReferenceValue as DragonStateBase);
                    }

                    float comparitorOffset = 0;
                    if (isNumber.boolValue)
                    {
                        comparitorOffset = 100;
                        var numberComparisonRect = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 5) * 2, 95, EditorGUIUtility.singleLineHeight);
                        numberComparitorType.enumValueIndex = (int)(DragonStateComparisonNumber)EditorGUI.EnumPopup(numberComparisonRect, (DragonStateComparisonNumber)numberComparitorType.enumValueIndex);
                    }
                    var comparitorRect = new Rect(position.x + comparitorOffset, position.y + (EditorGUIUtility.singleLineHeight + 5) * 2, position.width - comparitorOffset, EditorGUIUtility.singleLineHeight);
                    ComparitorField(comparitorRect, property, newStateValueType);
                }
            }
            EditorGUI.EndFoldoutHeaderGroup();
            
            EditorGUI.EndProperty();
        }
    }
}