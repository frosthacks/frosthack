using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SplitEnemy : Enemy
{
    public GameObject baby;
    public int spawns;
    [Server]
    public override void OnDie()
    {
        base.OnDie();
        PathTraveller path = gameObject.GetComponent<PathTraveller>();
        
        for(int i = 0; i < spawns; i++)
        {
            
            GameObject spawn = Instantiate(baby, transform.position, Quaternion.identity);
            PathTraveller spawnPath = spawn.GetComponent<PathTraveller>();
            spawnPath.destinationNode = path.destinationNode;
            spawnPath.attacking = path.attacking;
            spawnPath.creator = path.creator;

           
            NetworkServer.Spawn(spawn);
            Debug.Log("spawning" + i);
        }

    }
}
