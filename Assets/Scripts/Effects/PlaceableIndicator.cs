using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlaceableIndicator : MonoBehaviour
{
    static Color unobstructedColor = new Color(0, 1, 0, 0.5f);
    static Color obstructedColor = new Color(1, 0, 0, 0.5f);

    public bool isObstructed = false;
    GameObject attackRadiusCircle;
    GameObject placeRadiusCircle;

    void Start() {
        attackRadiusCircle = transform.Find("AttackRadius").gameObject;
        placeRadiusCircle = transform.Find("PlaceRadius").gameObject;
    }

    void OnTriggerEnter2D(Collider2D collisionInfo) {
        isObstructed = true; 
        Debug.Log("entered collider");
        placeRadiusCircle.GetComponent<SpriteRenderer>().color = PlaceableIndicator.obstructedColor;
    }

    void OnTriggerExit2D(Collider2D collisionInfo) {
       isObstructed = false;
        Debug.Log("exited collider");
       placeRadiusCircle.GetComponent<SpriteRenderer>().color = PlaceableIndicator.unobstructedColor;
    }
}
