using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BookInterface))]
public class BookInterfaceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BookInterface bookInterface = (BookInterface)target;

        GUI.enabled = Application.isPlaying;
        if (GUILayout.Button("Toggle Open/Close"))
        {
            bookInterface.openClose.ToggleOpenClose();
        }
        GUI.enabled = true;
    }
}
