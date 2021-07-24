using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarComparator : Comparator
{
    public FarComparator(Transform transform) : base(transform)
    {
    }

    public override int SortBy(GameObject enemy1, GameObject enemy2)
    {
        return Vector3.Distance(transform.position, enemy1.transform.position).CompareTo(Vector3.Distance(transform.position, enemy2.transform.position));
    }
}
