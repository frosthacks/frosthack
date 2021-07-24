using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Projectile : NetworkBehaviour
{
    public ProjectileData data;
    public float delta;
    public Rigidbody2D rb;
    public Vector2 iniPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        iniPos = transform.position;
        
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
        enemy.data.health -= data.damage;
        data.durability -= 1;
        if (enemy.data.health < 0)
        {
            Destroy(collision.gameObject);
        }
        if (data.durability <= 0)
        {
            Destroy(gameObject);

        }
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
