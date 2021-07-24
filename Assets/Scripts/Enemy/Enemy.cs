using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    SpriteRenderer spriteRenderer;

    void Start() {
   
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        
    }

}
