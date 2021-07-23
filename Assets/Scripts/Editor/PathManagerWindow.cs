using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathManagerWindow : EditorWindow
{
    [MenuItem("Tools/Path Editor")]
    public static void Open() {
        GetWindow<PathManagerWindow>();
    }

    public Transform pathRoot;

    private void OnGUI() {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("pathRoot"));

        if (pathRoot == null) {
            EditorGUILayout.HelpBox("Please select a path root", MessageType.Warning);
        } else {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons() {
        if (GUILayout.Button("New Path Node")) {
            CreatePathNode(); 
        } 
    }

    void CreatePathNode() {
        GameObject newPathNode = new GameObject("pathnode" + pathRoot.childCount, typeof(PathNode));
        newPathNode.transform.SetParent(pathRoot, false);

        // automatically set previous node's next node reference
        if (pathRoot.childCount > 1) {
            PathNode prevNode = pathRoot.GetChild(pathRoot.childCount-2).GetComponent<PathNode>();
            prevNode.nextNode = newPathNode.GetComponent<PathNode>();
        }

        Selection.activeGameObject = newPathNode;

    }
}
