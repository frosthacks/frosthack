using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class TowerData : ScriptableObject
{
    public new string name;
    public string description;
    public int range;
    public float placeRadius;
    public int atkSpeed;
    public GameObject projectile;
    public List<string> upgrades;
    public bool homing;
    
    
    
}
