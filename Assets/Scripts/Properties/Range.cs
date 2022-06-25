using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct Range
{
    public float min;
    public float max;
}

[CustomPropertyDrawer(typeof(Range))]
public class RangeDrawer : PropertyDrawer
{
    private const float SubLabelSpacing = 4;
    private const float BottomSpacing = 2;

    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        pos.height -= BottomSpacing;
        label = EditorGUI.BeginProperty(pos, label, prop);
        Rect contentRect = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
        GUIContent[] labels = new[] { new GUIContent("Min"), new GUIContent("Max") };
        SerializedProperty[] properties = new[] { prop.FindPropertyRelative("min"), prop.FindPropertyRelative("max") };
        DrawMultiplePropertyFields(contentRect, labels, properties);

        EditorGUI.EndProperty();
    }

    // public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    // {
    //     return base.GetPropertyHeight(property, label) + BottomSpacing;
    // }


    private static void DrawMultiplePropertyFields(Rect pos, GUIContent[] subLabels, SerializedProperty[] props)
    {
        // backup gui settings
        int originalIndentLevel = EditorGUI.indentLevel;
        float originalLabelWidth = EditorGUIUtility.labelWidth;

        // draw properties
        int propsCount = props.Length;
        float width = (pos.width - (propsCount - 1) * SubLabelSpacing) / propsCount;
        Rect contentPos = new(pos.x, pos.y, width, pos.height);
        EditorGUI.indentLevel = 0;
        for (int i = 0; i < propsCount; i++)
        {
            EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(subLabels[i]).x;
            EditorGUI.PropertyField(contentPos, props[i], subLabels[i]);
            contentPos.x += width + SubLabelSpacing;
        }

        // restore gui settings
        EditorGUIUtility.labelWidth = originalLabelWidth;
        EditorGUI.indentLevel = originalIndentLevel;
    }
}
