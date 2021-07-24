using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Projectile : NetworkBehaviour
{
    public NetworkPlayer creator;
    public ProjectileData data;
    public float delta;
    public Rigidbody2D rb;
    public Vector2 iniPos;
    public int durability;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        iniPos = transform.position;
        durability = data.durability;
        
    }
    
    [Server]
    public void Shoot(Vector3 target)
    {
        Vector2 moveDirection = (target - transform.position).normalized*data.speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

    }

    [Server]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        enemy.health -= data.damage;
        durability -= 1;
        Act();
        if (enemy.health <= 0)
        {
            creator.incrementMoney(enemy.data.reward);
            Destroy(collision.gameObject);
        }
        if (durability <= 0)
        {
            if (data.aoe)
            {
                Destroy(gameObject, 0.05f);
                
            }
            else
            {
                Destroy(gameObject);

            }
            

        }
    }
    [Server]
    public void Act()
    {

    }

    [Server]
    public void Update()
    {
        if (Vector2.Distance(transform.position, iniPos) > data.maxDistance)
        {
            Destroy(gameObject);
        }
    }
    
}
