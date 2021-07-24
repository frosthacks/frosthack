using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    GameObject tower;

    void Start() {
        
    }

    void Update() {

        if (tower!=null) {
            tower.transform.position = ScreenToWorld(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)) {
                PlaceableIndicator indicator = tower.GetComponentInChildren<PlaceableIndicator>();

                // don't place if obstructed
                if (indicator.isObstructed) return;

                tower.GetComponent<Tower>().placed = true;
                Destroy(indicator.gameObject);
                tower = null;
                
            }
            if (Input.GetMouseButtonDown(1)) {
                Destroy(tower);
                tower = null;
            }
        }
    }

    public void OnCreate(GameObject prefab) {
        tower = Instantiate(prefab, ScreenToWorld(Input.mousePosition), Quaternion.identity);
        GameObject placeIndicator = (GameObject)Instantiate(Resources.Load("Effects/BuildIndicator"));
        placeIndicator.transform.SetParent(tower.transform, false);

        PlaceableIndicator indicator = placeIndicator.GetComponent<PlaceableIndicator>(); 
        indicator.setRadius(tower.GetComponent<Tower>().data.range, tower.GetComponent<Tower>().data.placeRadius);
    }

    public Vector3 ScreenToWorld(Vector3 mousePos) {

        Vector3 stw = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return new Vector3(stw.x, stw.y, 0);

    }
}
