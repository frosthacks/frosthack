using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class PathVisualizer
{

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(PathNode pathNode, GizmoType gizmoType) {
        if ((gizmoType & GizmoType.Selected) != 0) {
            Gizmos.color = Color.blue;
        } else {
            Gizmos.color = Color.yellow;
        }

        Gizmos.DrawSphere(pathNode.transform.position, 0.1f);

        Gizmos.color = Color.green * 0.5f;
        Gizmos.DrawSphere(pathNode.transform.position, PathNode.stoppingRange);

        // draw line connecting current node to next node
        if (pathNode.nextNode != null) {

            Gizmos.color = Color.white;
            Gizmos.DrawLine(pathNode.transform.position, pathNode.nextNode.transform.position);

        }


    }

}
