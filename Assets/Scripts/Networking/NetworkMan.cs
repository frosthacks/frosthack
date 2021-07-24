using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct CreateLobbyPlayerMessage : NetworkMessage
{
    public string name;
}

public enum gameStatus { lobby, ongoing };

public class NetworkMan : NetworkManager
{
    public GameHandler game;
    public LobbyHandler lobby;

    // Server Sided functionalities
    Dictionary<NetworkConnection, string> players = new Dictionary<NetworkConnection, string>();
    gameStatus status = gameStatus.lobby;
    StateManager currentState;

    private new void Start()
    {
        currentState = lobby;
    }

    public void changeRoomState()
    {
        status = status == gameStatus.lobby ? gameStatus.ongoing : gameStatus.lobby;
        if (status == gameStatus.lobby)
        {
            currentState = lobby;
        }
        else
        {
            currentState = game;
        }

        Dictionary<NetworkIdentity, string> beginTable = new Dictionary<NetworkIdentity, string>();
        foreach(KeyValuePair<NetworkConnection, string> conn in players)
        {
            beginTable.Add(conn.Key.identity, conn.Value);
        }

        currentState.onBegin(beginTable);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreateLobbyPlayerMessage>(OnCreateCharacter);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        players.Add(conn, "John Doe");
        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        players.Remove(conn);
        base.OnServerDisconnect(conn);
        currentState.onLeave(conn);
    }

    void OnCreateCharacter(NetworkConnection conn, CreateLobbyPlayerMessage message)
    {
        players[conn] = message.name;
        currentState.onEnter(conn, message.name);

    }

    // Client Sided functionalities 
    public NetworkHandler handler;
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        // you can send the message here, or wherever else you want
        CreateLobbyPlayerMessage characterMessage = new CreateLobbyPlayerMessage
        {
            name = handler.username
        };

        conn.Send(characterMessage);
    }
}
