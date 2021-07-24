using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Tower : MonoBehaviour
{
    public TowerData data;
    public bool placed = false;
    public float delta = 0;
    public Vector2 enemyPosition;
    public GameObject enemies;

    public ComparatorPriority priority = ComparatorPriority.Close;
    public CloseComparator closeComparator;
    public FarComparator farComparator;
    public StrongComparator strongComparator;
    public WeakComparator weakComparator;

    public Vector3 target;
    public bool targetExists;

    void Start()
    {
        enemies = GameObject.Find("/Enemies");
        closeComparator = new CloseComparator(transform);
        farComparator = new FarComparator(transform);
        strongComparator = new StrongComparator(transform);
        weakComparator = new WeakComparator(transform);
    }
    void FindTarget()
    {
        targetExists = true;
        List<GameObject> inRadius = new List<GameObject>();
        foreach (Transform enemy in enemies.transform)
        {
            if (Vector2.Distance(transform.position, enemy.position) < data.range)
            {
                inRadius.Add(enemy.gameObject);

            }
        }
        if (inRadius.Count == 0)
        {
            targetExists = false;
  

            return;
        }

        Comparator comparator = closeComparator;
        switch (priority)
        {
            case ComparatorPriority.Close:
                {
                    
                    comparator = closeComparator;
                    break;
                }
            case ComparatorPriority.Far:
                {
                    comparator = closeComparator;
                    break;
                }
            case ComparatorPriority.Strong:
                {
                    comparator = closeComparator;
                    break;
                }
            case ComparatorPriority.Weak:
                {
                    comparator = closeComparator;
                    break;
                }
        }
        
        inRadius.Sort((p1, p2) => comparator.SortBy(p1, p2));
        target = inRadius[0].transform.position;
    }
    
    void FixedUpdate()
    {
        
        if (!placed)
        {
            return;
        }

        act();
    }
    public void act()
    {
        delta += Time.deltaTime;
        if (delta > data.atkSpeed)
        {
            FindTarget();
            if (!targetExists)
            {
                return;
            }
            delta = 0;
            GameObject projectile = Instantiate(data.projectile, transform.position, Quaternion.identity);

            NetworkServer.Spawn(projectile);

            projectile.transform.SetParent(GameObject.Find("/Projectiles").transform);
            projectile.GetComponent<Projectile>().Shoot(target);
            //projectile.transform.LookAt(Vector3.zero);
        }
    }

}
