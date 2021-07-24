using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Tower : NetworkBehaviour
{
    public NetworkPlayer creator;
    public TowerData data;
    public bool placed = false;
    public float delta = 0;
    public Vector2 enemyPosition;
    public GameObject[] enemies;

    public ComparatorPriority priority = ComparatorPriority.Close;
    public CloseComparator closeComparator;
    public FarComparator farComparator;
    public StrongComparator strongComparator;
    public WeakComparator weakComparator;

    public Vector3 target;
    public bool targetExists;

    void Start()
    {
        closeComparator = new CloseComparator(transform);
        farComparator = new FarComparator(transform);
        strongComparator = new StrongComparator(transform);
        weakComparator = new WeakComparator(transform);
    }

    [Server]
    void FindTarget()
    {
        targetExists = true;
        List<GameObject> inRadius = new List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) < data.range)
            {
                inRadius.Add(enemy);

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
    
    [Server]
    void FixedUpdate()
    {
        
        if (!placed)
        {
            return;
        }

        act();
    }

    [Server]
    public void act()
    {

        delta += Time.deltaTime;
        if (delta > data.atkSpeed)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            FindTarget();
            if (!targetExists)
            {
                return;
            }
            delta = 0;
            GameObject projectile = Instantiate(data.projectile, transform.position, Quaternion.identity);
            NetworkServer.Spawn(projectile);

            Projectile shot = projectile.GetComponent<Projectile>();
            shot.creator = creator;
            shot.Shoot(target);
            //projectile.transform.LookAt(Vector3.zero);
        }
    }


    public override void OnStartClient()
    {
        base.OnStartClient();

        gameObject.AddComponent<TowerHoverHighlight>();
    }

}
