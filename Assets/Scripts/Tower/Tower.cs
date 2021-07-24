using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;
    public bool placed = false;
    public float delta = 0;
    public Vector2 enemyPosition;
    public GameObject enemies;
    

    




    
   
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("/Enemies");
        

        



        
    }
    void FindTarget()
    {
        List<Transform> inRadius = new List<Transform>() ;
        foreach(Transform enemy in enemies.transform)
        {
            if (Vector2.Distance(transform.position, enemy.position)<data.range)
            {
                inRadius.Add(enemy);

            }
        }



    }

    // Update is called once per frame
    
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
