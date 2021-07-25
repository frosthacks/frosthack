using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Enemy : NetworkBehaviour
{
    public EnemyData data;
    SpriteRenderer spriteRenderer;
    public int health;
    public GameObject particleEffect;


    void Start() {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        health = data.health;

    }

    [Server]
    public virtual void OnDie(){ 

        if(particleEffect!=null){

            Instantiate(particleEffect, transform.position, Quaternion.identity);
        }


    }

}
