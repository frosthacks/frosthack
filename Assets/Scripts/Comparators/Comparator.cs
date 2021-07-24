using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ComparatorPriority
{
    Close, Far, Strong, Weak

};
abstract public class Comparator
{
    public Transform transform;

    public Comparator(Transform transform)
    {
        this.transform = transform;

    }
    public abstract int SortBy(GameObject enemy1, GameObject enemy2);
}
