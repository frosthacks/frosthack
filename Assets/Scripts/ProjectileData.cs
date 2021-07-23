using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
public class ProjectileData : ScriptableObject
{
    public new string name;
    public string description;
    public int speed;
    public int duration;
    public int damage;


}
