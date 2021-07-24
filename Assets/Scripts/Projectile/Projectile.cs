using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;
    public float delta;
    
    // Start is called before the first frame update
    void Start()
    {

        
    }
    

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta < data.duration)
        {
            transform.Translate(transform.forward * data.speed);


        }
        else
        {
            Destroy(this);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
