using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : StateManager
{
    public GameObject playerSlots;
    List<NetworkIdentity> allPlayers;
    public override void onBegin(Dictionary<NetworkIdentity, string> players)
    {
        allPlayers = new List<NetworkIdentity>();
        int c = 0;
        foreach (KeyValuePair<NetworkIdentity, string> plr in players)
        {
            NetworkPlayer netPlr = plr.Key.gameObject.GetComponent<NetworkPlayer>();
            allPlayers.Add(plr.Key);

            c++;
        }
    }

    public override void onEnter(NetworkConnection conn, string username)
    {
        throw new System.NotImplementedException();
    }

    public override void onLeave(NetworkConnection conn)
    {
        throw new System.NotImplementedException();
    }

}
