using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public new string name;
    public string description;
    public float speed;
    public int reward;
    public int damage;
    public int health;
    public int cost;
}

