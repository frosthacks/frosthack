using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

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

        updateClients();
    }

    public override void onEnter(NetworkConnection conn, string username)
    {
        allPlayers.Add(conn.identity, username);
        updateClients();
    }

    public override void onLeave(NetworkConnection conn)
    {
        allPlayers.Remove(conn.identity);
        if (readyPlayers.Contains(conn.identity))
        {
            readyPlayers.Remove(conn.identity);
        }

        updateClients();
    }

    // Server functions
    private void updateClients()
    {
        string[] names = new string[allPlayers.Count];
        bool[] actives = new bool[allPlayers.Count];
        int i = 0;

        foreach (KeyValuePair<NetworkIdentity, string> plr in allPlayers)
        {
            names[i] = plr.Value;
            actives[i] = readyPlayers.Contains(plr.Key);
            i++;
        }

        RpcDisplayCount(names, actives);
    }

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

        updateClients();

        if (readyPlayers.Count == allPlayers.Count)
        {
            Debug.Log("Time to start game");
            game.onBegin(allPlayers);
        }
    }

    // Client functions
    public GameObject EntranceMenu;
    public GameObject lobbyMenu;
    public GameObject lobbyPlayers;
    public GameObject buttonPrefab;

    Color yellow = new Color(255 / 255f, 253 / 255f, 116 / 255f);
    Color green = new Color(26 / 255f, 234 / 255f, 26 / 255f);

    [ClientRpc]
    public void RpcDisplayCount(string[] players, bool[] readyStates)
    {

        // make sure the correct UI is hidden/displayed
        if (EntranceMenu.activeSelf)
        {
            EntranceMenu.SetActive(false);
        }

        if (!lobbyMenu.activeSelf)
        {

            lobbyMenu.SetActive(true);
        }

        // make the player folder
        foreach (Transform child in lobbyPlayers.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, lobbyPlayers.transform);
            RectTransform rect = button.GetComponent<RectTransform>();
            rect.position = rect.position + new Vector3(0, -35 * i, 0);

            button.transform.GetChild(0).GetComponent<TMP_Text>().text = players[i];
            button.GetComponent<Image>().color = readyStates[i] ? green : yellow;
        }

        // check to see if we are finished
        bool finished = true;
        foreach (bool ready in readyStates)
        {
            if (!ready)
            {
                finished = false;
            }
        }

        if (finished)
        {
            lobbyMenu.SetActive(false);
        }
    }

    

    public void onReadyClick()
    {
        toggle();
    }
}
