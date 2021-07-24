using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;
    public bool placed = false;
    public float delta = 0;
    public Vector2 enemyPosition;

    void FixedUpdate() {

        if (!placed) {
            return;
        }

        delta += Time.deltaTime;
        if (delta > data.atkSpeed) {
            delta = 0;
            GameObject projectile = Instantiate(data.projectile,transform.position,Quaternion.identity);
            //projectile.transform.LookAt(Vector3.zero);
        }

    }
}
