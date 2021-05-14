using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatRangeSliderAttribute))]
public class FloatRangeSliderDrawer : PropertyDrawer 
{
    public override void OnGUI
    (Rect position, SerializedProperty property, GUIContent label) 
    {
        EditorGUI.BeginProperty(position, label, property);
            SerializedProperty minProperty = property.FindPropertyRelative("Min");
            SerializedProperty maxProperty = property.FindPropertyRelative("Max");
                float minValue = minProperty.floatValue;
                float maxValue = maxProperty.floatValue; 
                
        FloatRangeSliderAttribute limit = attribute as FloatRangeSliderAttribute;
        
        EditorGUI.MinMaxSlider
            (position, label, ref minValue, ref maxValue, limit.Min, limit.Max);
          
        minProperty.floatValue = minValue;
        maxProperty.floatValue = maxValue;

        EditorGUI.EndProperty();
    }
}