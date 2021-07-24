using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : StateManager
{
    Dictionary<NetworkIdentity, string> allPlayers;

    public override void onBegin(Dictionary<NetworkIdentity, string> players)
    {
        allPlayers = new Dictionary<NetworkIdentity, string>();
        foreach (KeyValuePair<NetworkIdentity, string> plr in players)
        {
            allPlayers.Add(plr.Key, plr.Value);
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
