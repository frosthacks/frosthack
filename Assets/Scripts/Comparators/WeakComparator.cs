using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakComparator : Comparator
{
    public WeakComparator(Transform transform) : base(transform)
    {
    }

    public override int SortBy(GameObject enemy1, GameObject enemy2)
    {
        return enemy1.GetComponent<Enemy>().data.health.CompareTo(enemy2.GetComponent<Enemy>().data.health);
    }
}
