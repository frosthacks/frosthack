using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    SpriteRenderer spriteRenderer;
    public int health;

    void Start() {
   
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        health = data.health;

    }


    void Update()
    {
        
    }
}
