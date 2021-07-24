using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongComparator : Comparator
{
    public StrongComparator(Transform transform) : base(transform)
    {
    }

    public override int SortBy(Enemy enemy1, Enemy enemy2)
    {
        throw new System.NotImplementedException();
    }
}
