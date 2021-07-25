using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProjectile : Projectile
{
    public GameObject particle;
    public override void OnDie()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }
}
