using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite img;
    public int range;
    public int atkSpeed;
    public int speed;
    public GameObject projectile;

    
    
}
