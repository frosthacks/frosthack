using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : Projectile
{
    new public void Act()
    {
        float randomAngle = Random.Range(0, 359)*Mathf.Deg2Rad;
        rb.velocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        

    }
}
