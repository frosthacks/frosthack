using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTraveller : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    public PathNode destinationNode;

    void Start() {
        
    }

    void Update() {

        if (destinationNode == null) return;

        Vector3 destVec = destinationNode.transform.position - transform.position;

        if (destVec.magnitude >= PathNode.stoppingRange+Random.Range(-0.1f, 0.1f)) {
            transform.Translate(destVec.normalized * moveSpeed * Time.deltaTime);
        } else {
            destinationNode = destinationNode.nextNode;
        }

    }

}
