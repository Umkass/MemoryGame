using StaticData.GameSettings;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(SliderSettingData))]
    public class SliderSettingDataDrawer : PropertyDrawer
    {
        private bool _foldout;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            _foldout = EditorGUI.Foldout(foldoutRect, _foldout, label, true);

            if (_foldout)
            {
                EditorGUI.indentLevel++;
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                var minProperty = property.FindPropertyRelative("min");
                var maxProperty = property.FindPropertyRelative("max");
                var defaultValueProperty = property.FindPropertyRelative("defaultValue");

                if (minProperty == null || maxProperty == null || defaultValueProperty == null)
                {
                    EditorGUI.LabelField(position, "Error: Can't find properties.");
                    EditorGUI.EndProperty();
                    return;
                }

                Rect minRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.IntSlider(minRect, minProperty, 0, maxProperty.intValue - 1, new GUIContent("Min"));
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                Rect maxRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.IntSlider(maxRect, maxProperty, minProperty.intValue + 1, 100, new GUIContent("Max"));
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                Rect defaultValueRect =
                    new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                if (defaultValueProperty.intValue > maxProperty.intValue)
                    defaultValueProperty.intValue = maxProperty.intValue;
                else if (defaultValueProperty.intValue < minProperty.intValue)
                    defaultValueProperty.intValue = minProperty.intValue;

                EditorGUI.IntSlider(defaultValueRect, defaultValueProperty, minProperty.intValue, maxProperty.intValue,
                    new GUIContent("Default Value"));


                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_foldout)
                return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 4;
            
            return EditorGUIUtility.singleLineHeight;
        }
    }
}