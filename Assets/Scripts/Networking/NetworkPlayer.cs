using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkPlayer : NetworkBehaviour
{
    PlayerData dataSingleton;

    // initialize these variables and they replicate
    [SyncVar]
    public string username;

    [SyncVar]
    public Vector3 location;

    // Hp synchronization calls a client hook to handle UI
    [SyncVar(hook = nameof(healthChanged))]
    public int hp = 100;

    // gets synchronized locally with targetRPCs
    public int money; 

    

    void healthChanged()
    {
        dataSingleton.hp = hp;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
