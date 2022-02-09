using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyController : MonoBehaviour
{
    //public List<AttackPattern> attacks = new List<AttackPattern>();
    public AttackPattern attack;    
}

[System.Serializable]
public class AttackPattern
{
    public int length, wait;
    public List<List<bool>> notes = new List<List<bool>>();
}

/*[CustomPropertyDrawer(typeof(AttackPattern))]
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
*/