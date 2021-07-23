using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimation : MonoBehaviour
{

    public float tiltMagnitude = 30.0f; // in degree
    public float tiltSpeed = 1.0f;

    private float distanceTravelled = 0.0f;
    private Vector3 prevPos;

    void Start() {
        prevPos = transform.position;
    }

    void Update() {

        distanceTravelled += (transform.position - prevPos).magnitude;
        prevPos = transform.position;

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            tiltMagnitude * Mathf.Sin(tiltSpeed * distanceTravelled)
        );

    }
}
