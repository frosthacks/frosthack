using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class PathVisualizer
{

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(PathNode pathNode, GizmoType gizmoType) {
        if (pathNode.nextNodes.Count == 0) {
            Gizmos.color = Color.red;
        } else {
            Gizmos.color = Color.yellow;
        }

        if ((gizmoType & GizmoType.Selected) != 0) Gizmos.color = Color.blue;

        Gizmos.DrawSphere(pathNode.transform.position, 0.1f);

        Gizmos.color = Color.green * 0.5f;
        Gizmos.DrawSphere(pathNode.transform.position, PathNode.stoppingRange);

        // draw line connecting current node to next node
        Gizmos.color = Color.white;
        for (int i = 0; i < pathNode.nextNodes.Count; i++) {
           Gizmos.DrawLine(pathNode.transform.position, pathNode.nextNodes[i].transform.position);
        }

    }

}
