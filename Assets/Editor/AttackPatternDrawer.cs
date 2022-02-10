using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AttackPattern))]
public class AttackPatternDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        SerializedProperty length = property.FindPropertyRelative("length");

        Rect newPos = position;
        //newPos.y += 18f;

        EditorGUI.PrefixLabel(position, label);

        newPos.y += 18f;
        SerializedProperty lanes = property.FindPropertyRelative("lanes");

        for (int i = 0; i < 4; i++)
        {
            SerializedProperty notes = lanes.GetArrayElementAtIndex(i).FindPropertyRelative("notes");
            newPos.height = 18f;
            notes.arraySize = length.intValue;
            newPos.width = position.width / 4;
            for (int j = 0; j < length.intValue; j++)
            {
                EditorGUI.PropertyField(newPos, notes.GetArrayElementAtIndex(j), GUIContent.none);
                //newPos.x += newPos.width;
                newPos.x += 18f;
            }
            newPos.x = position.x;
            newPos.y += 18f;
        }
        length.intValue = EditorGUI.IntField(newPos, length.intValue);

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 10;
    }
}
