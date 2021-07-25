using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitEnemy : Enemy
{
    public GameObject baby;
    public int spawns;
    public override void OnDie()
    {
        base.OnDie();
        for(int i = 0; i < spawns; i++)
        {
            Instantiate(baby, transform.position, Quaternion.identity);
        }

    }
}
