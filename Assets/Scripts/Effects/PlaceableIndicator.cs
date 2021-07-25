using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlaceableIndicator : MonoBehaviour
{
    static Color unobstructedColor = new Color(0, 1, 0, 0.5f);
    static Color obstructedColor = new Color(1, 0, 0, 0.5f);
    private CameraController cam;
    

    public bool isObstructed = false;
    GameObject attackRadiusCircle;
    GameObject placeRadiusCircle;

    void Awake() {
        attackRadiusCircle = transform.Find("AttackRadius").gameObject;
        placeRadiusCircle = transform.Find("PlaceRadius").gameObject;
        
        
    }
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    public void setRadius(float attackRadius, float placeRadius) {
        attackRadiusCircle.transform.localScale = new Vector3(attackRadius, attackRadius, 1);
        placeRadiusCircle.transform.localScale = new Vector3(placeRadius, placeRadius, 1);
        GetComponentInChildren<CircleCollider2D>().radius = placeRadius/2;
    }
    private void Update()
    {
        if (!cam.InBounds(transform.position))
        {
            isObstructed = true;
        }
        


    }
    
    void OnTriggerEnter2D(Collider2D collisionInfo) {
   
        isObstructed = true; 
        placeRadiusCircle.GetComponent<SpriteRenderer>().color = PlaceableIndicator.obstructedColor;
    }

    void OnTriggerExit2D(Collider2D collisionInfo) {
        if (!cam.InBounds(transform.position))
        {
            isObstructed = true;
            return;
        }
       isObstructed = false;
       placeRadiusCircle.GetComponent<SpriteRenderer>().color = PlaceableIndicator.unobstructedColor;
    }
}
