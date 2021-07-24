using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tower : MonoBehaviour
{
    public TowerData data;
    public bool placed = false;
    public float delta = 0;
    public Vector2 enemyPosition;

    void Start()
    {
        GetComponent<CircleCollider2D>().radius = data.placeRadius/2;
        
    }
    void findTarget()
    {

    }

    void FixedUpdate()
    {
        if (!placed)
        {
            return;
        }
        delta += Time.deltaTime;
        if (delta > data.atkSpeed)
        {
            delta = 0;
            GameObject projectile = Instantiate(data.projectile,transform.position,Quaternion.identity);
            projectile.transform.SetParent(GameObject.Find("/Projectiles").transform);
            projectile.GetComponent<Projectile>().Shoot(Vector3.zero);
            //projectile.transform.LookAt(Vector3.zero);
        }




    }
}
