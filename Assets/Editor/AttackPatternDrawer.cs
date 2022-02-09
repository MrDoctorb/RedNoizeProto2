using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AttackPatternAttribute))]
public class AttackPatternDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical();
            {
                // your elements column 1
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            {
                // your elements column 2
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            {
                // your elements column 3
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            {
                // your elements column 4
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}
