using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public const float stoppingRange = 0.1f; // range for when an entity is considered 'at' the node

    public List<PathNode> nextNodes = new List<PathNode>();

    public PathNode getNextNode() {
        if (nextNodes.Count == 0) return null;
        return nextNodes[Random.Range(0, nextNodes.Count)];
    }

}
