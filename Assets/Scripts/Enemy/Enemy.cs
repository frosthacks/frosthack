using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    SpriteRenderer spriteRenderer;
    public int health;
    public GameObject particleEffect;


    void Start() {
   
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        health = data.health;

    }
    public virtual void OnDie()
    {
        Instantiate(particleEffect, transform.position, Quaternion.identity);


    }


    void Update()
    {
        
    }
}
