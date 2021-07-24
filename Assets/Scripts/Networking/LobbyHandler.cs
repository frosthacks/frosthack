using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class LobbyHandler : StateManager
{
    public GameHandler game;
    Dictionary<NetworkIdentity, string> allPlayers = new Dictionary<NetworkIdentity, string>();
    HashSet<NetworkIdentity> readyPlayers = new HashSet<NetworkIdentity>();
    // State functionalities
    public override void onBegin(Dictionary<NetworkIdentity, string> players)
    {
        allPlayers = new Dictionary<NetworkIdentity, string> ();
        foreach (KeyValuePair<NetworkIdentity, string> plr in players)
        {
            allPlayers.Add(plr.Key, plr.Value);
        }

        RpcDisplayCount(readyPlayers.Count, allPlayers.Count);
    }

    public override void onEnter(NetworkConnection conn, string username)
    {
        allPlayers.Add(conn.identity, username);
        RpcDisplayCount(readyPlayers.Count, allPlayers.Count);
    }

    public override void onLeave(NetworkConnection conn)
    {
        allPlayers.Remove(conn.identity);
        if (readyPlayers.Contains(conn.identity))
        {
            readyPlayers.Remove(conn.identity);
        }

        RpcDisplayCount(readyPlayers.Count, allPlayers.Count);
    }

    // Server functions
    [Command(requiresAuthority = false)]
    public void toggle(NetworkConnectionToClient sender = null)
    {
        if (readyPlayers.Contains(sender.identity))
        {
            readyPlayers.Remove(sender.identity);
        }
        else
        {
            readyPlayers.Add(sender.identity);
        }
        RpcDisplayCount(readyPlayers.Count, allPlayers.Count);

        if (readyPlayers.Count == allPlayers.Count)
        {
            Debug.Log("Time to start game");
            game.onBegin(allPlayers);
        }
    }

    // Client functions
    public TMP_Text readyText;

    [ClientRpc]
    public void RpcDisplayCount(int active, int total)
    {
        if (active == total) // switch for now
        {
            lobbyMenu.SetActive(false);
        }

        readyText.text = "Ready (" + active + "/" + total + ")";
    }

    Color green = new Color(26/255f, 234/255f, 26/255f);
    Color yellow = new Color(255/255f, 253/255f, 116/255f);
    bool ready = false;
    public void onReadyClick()
    {
        ready = !ready;
        readyText.color = ready ? green : yellow;
        toggle();
    }

    public GameObject EntranceMenu;
    public GameObject lobbyMenu;
    public override void OnStartClient()
    {
        base.OnStartClient();
        EntranceMenu.SetActive(false);
        lobbyMenu.SetActive(true);
    }
}
