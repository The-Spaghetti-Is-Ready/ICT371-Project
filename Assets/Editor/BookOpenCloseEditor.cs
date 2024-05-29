#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BookOpenClose))]
public class BookOpenCloseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BookOpenClose bookOpenClose = (BookOpenClose)target;

        GUI.enabled = Application.isPlaying;
        if (GUILayout.Button("Toggle Open/Close"))
        {
            bookOpenClose.ToggleOpenClose();
        }
        GUI.enabled = true;
    }
}

#endif
