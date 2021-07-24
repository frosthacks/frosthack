using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct playerData{
    public string username;
    public Vector3 mapCenter;
    public int money;
}

public class GameHandler : StateManager
{
    public GameObject playerSlots;
    Dictionary<NetworkIdentity, playerData> allPlayers;

    public override void onBegin(Dictionary<NetworkIdentity, string> players)
    {
        allPlayers = new Dictionary<NetworkIdentity, playerData>();
        int c = 0;
        foreach (KeyValuePair<NetworkIdentity, string> plr in players)
        {
            playerData newPlr = new playerData {
                username = plr.Value,
                mapCenter = playerSlots.transform.GetChild(c).position,
                money = 200
            };

            allPlayers.Add(plr.Key, newPlr);
            SetPlayerData(plr.Key.connectionToClient, newPlr);
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


    // Client Sided
    public GameObject mainCamera;
    public playerData clientPlayer;

    [TargetRpc]
    public void SetPlayerData(NetworkConnection target, playerData newinfo)
    {
        clientPlayer = newinfo;
        mainCamera.transform.position = newinfo.mapCenter + new Vector3(0, 0, -10);
    }
}
