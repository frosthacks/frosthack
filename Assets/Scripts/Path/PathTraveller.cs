using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Enemy))]
public class PathTraveller : NetworkBehaviour
{
    public PathNode destinationNode;
    public NetworkPlayer attacking;
    public NetworkPlayer creator;

    void Start() {
        
    }

    void Update() {
        if (isServer)
        {
            if (destinationNode == null)
            {
                handleEndOfPath();
                return;
            }

            Vector3 destVec = destinationNode.transform.position - transform.position;

            if (destVec.magnitude >= PathNode.stoppingRange + Random.Range(-0.1f, 0.1f))
            {
                float moveSpeed = GetComponent<Enemy>().data.speed;
                transform.Translate(destVec.normalized * moveSpeed * Time.deltaTime);
            }
            else
            {
                destinationNode = destinationNode.getNextNode();
            }
        }

        

    }

    [Server]
    void handleEndOfPath()
    {
        EnemyData data = GetComponent<Enemy>().data;

        if (attacking != null)
        {
            attacking.hp -= data.damage;
        }

        if (creator != null)
        {
            Debug.Log("Reward creator");
        }

        NetworkServer.Destroy(gameObject); 

        UserManager.Global.takeDamage(GetComponent<Enemy>().data.damage);
    }


}
