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

        // GUI.enabled = (Selection.activeGameObject?.GetComponent<PathNode>() != null);
        // if (GUILayout.Button("Split Node Here")) {
        //     CreatePathNode(Selection.activeGameObject.GetComponent<PathNode>()); 
        // }

        // GUI.enabled = true;
    }

    void CreatePathNode() {
        GameObject newPathNode = new GameObject("pathnode" + pathRoot.childCount, typeof(PathNode));
        newPathNode.transform.SetParent(pathRoot, false);

        // automatically set previous node's next node reference
        if (pathRoot.childCount > 1) {

            PathNode prevNode = pathRoot.GetChild(pathRoot.childCount-2).GetComponent<PathNode>();
            if (Selection.activeGameObject?.GetComponent<PathNode>() != null) {
                prevNode = Selection.activeGameObject.GetComponent<PathNode>();
            }
            newPathNode.transform.position = prevNode.transform.position + new Vector3(1, 0, 0);
            prevNode.nextNodes.Add(newPathNode.GetComponent<PathNode>());
        }


        Selection.activeGameObject = newPathNode;

    }


}
