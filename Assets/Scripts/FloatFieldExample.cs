using UnityEngine;
using UnityEditor;

public class FloatFieldExample : MonoBehaviour
{
    public bool flag;
    public int i = 1;
}

[CustomEditor(typeof(FloatFieldExample))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as FloatFieldExample;

        myScript.flag = GUILayout.Toggle(myScript.flag, "Flag");

        if (myScript.flag)
            myScript.i = EditorGUILayout.IntSlider("I field:", myScript.i, 1, 100);

    }
}