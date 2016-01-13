using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(Playback))]
public class PlaybackEditor : Editor
{    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawProperty("sequenceFolder", "Sequence Folder:");
        DrawProperty("FPS", "FPS:");
        DrawProperty("playOnAwake", "Play On Awake");
        if (serializedObject.FindProperty("playOnAwake").boolValue) DrawProperty("startDelay", "Start Delay:");
        DrawProperty("loop", "Loop");
        DrawProperty("mode", "Mode:");
        if (serializedObject.FindProperty("mode").enumValueIndex == 0) DrawProperty("playbackMat", "Playback Material:");
        if (serializedObject.FindProperty("mode").enumValueIndex == 2) DrawProperty("rawImage", "Raw Image:");
        
        EditorGUILayout.Separator();
        DrawProperty("playAudio", "Play Audio");
        if (serializedObject.FindProperty("playAudio").boolValue)
        {
            DrawProperty("source", "Audio Source:");
            DrawProperty("clip", "Clip:");
            DrawProperty("clipBaseFps", "Base FPS:");
        }

        EditorGUILayout.Separator();
        DrawProperty("showDebug", "Debug Info");
        if (serializedObject.FindProperty("showDebug").boolValue)
        {
            if (Application.isPlaying && Application.isEditor)
            {
                Playback t = (Playback)target;
                EditorGUILayout.SelectableLabel("Progress: " + (t.progress * 100f).ToString("f0") + "%");
                EditorGUILayout.SelectableLabel("Frames: " + t.frames + "  Current: " + (t.currentFrame+1));
                this.Repaint();
            }
            else EditorGUILayout.SelectableLabel("Enter playmode.");
        }
    }

    void DrawProperty(string name, string label)
    {
        SerializedProperty prop = serializedObject.FindProperty(name);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(prop, new GUIContent(label), true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        EditorGUIUtility.LookLikeControls();
    }
}