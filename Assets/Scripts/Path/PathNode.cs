using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public const float stoppingRange = 0.5f; // range for when an entity is considered 'at' the node

    public PathNode nextNode;

}
