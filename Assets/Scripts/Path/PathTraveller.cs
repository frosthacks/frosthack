using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTraveller : MonoBehaviour
{
    public float moveSpeed;

    [SerializeField] private PathNode destinationNode;

    void Start() {
        
    }

    void Update() {

        Vector3 destVec = destinationNode.transform.position - transform.position;
        if (destVec.magnitude >= PathNode.stoppingRange) {
            transform.Translate(destVec.normalized * moveSpeed * Time.deltaTime);
        }

    }

}
