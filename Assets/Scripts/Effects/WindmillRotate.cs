using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillRotate : MonoBehaviour
{

    public float rotateSpeed;

    void Update() {
       transform.Rotate(new Vector3(0, 0, - rotateSpeed * Time.deltaTime)); 
    }
}
