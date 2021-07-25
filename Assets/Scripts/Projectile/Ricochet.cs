using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : Projectile
{
    public override void Act()
    {
        Debug.Log("Ricochet");
        float randomAngle = Random.Range(0, 359)*Mathf.Deg2Rad;
        rb.velocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        

    }
}
