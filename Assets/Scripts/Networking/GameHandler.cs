using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameHandler : StateManager
{
    List<NetworkIdentity> allPlayers;
    public GameObject extraPlayerUIPrefab;
    public PlayerUIInfo localPlayerUI;
    public UserManager singletonDataStorage;
    public GameObject mainCamera;
    public GameObject playersUI;
    public GameObject mapSlots;
    public TMP_Text roundText;

    [SyncVar(hook = nameof(roundChanged))]
    public float round = 0.0f;

    // Server Init
    public override void onBegin(Dictionary<NetworkIdentity, string> players)
    {
        allPlayers = new List<NetworkIdentity>();
        int c = 0;
        foreach (KeyValuePair<NetworkIdentity, string> plr in players)
        {
            NetworkPlayer netPlr = plr.Key.gameObject.GetComponent<NetworkPlayer>();
            Transform playArea = mapSlots.transform.GetChild(c);
            netPlr.username = plr.Value;
            netPlr.location = playArea.position;
            Debug.Log("Server location shown " + playArea.position.ToString()); 
            Debug.Log(netPlr.username);
            allPlayers.Add(plr.Key);
            c++;
        }

        RpcInitializeGame(allPlayers.ToArray());

        round = 1.0f;
        startRound();
    }

    public override void onEnter(NetworkConnection conn, string username)
    {
        Debug.Log("Handle entering midgame");
    }

    public override void onLeave(NetworkConnection conn)
    {
        Debug.Log("Handle leaving");
    }

    // Client Init
    [ClientRpc]
    public void RpcInitializeGame(NetworkIdentity[] playerIDs)
    {
        // remove old player stuff
        foreach (Transform child in playersUI.transform)
        {
            Destroy(child.gameObject);
        }

        // link listener UIs
        int i = 0;
        foreach (NetworkIdentity id in playerIDs)
        {
            NetworkPlayer netPlr = id.gameObject.GetComponent<NetworkPlayer>();
            if (id.isLocalPlayer)
            {
                netPlr.dataSingleton = singletonDataStorage;
                netPlr.uiAdjust = localPlayerUI;

                Debug.Log("Set position correctly(?) at " + netPlr.location.ToString());
                mainCamera.transform.position = netPlr.location + new Vector3(0, 0, -10);

            }
            else
            { 
                // make new prefab
                GameObject plrUI = Instantiate(extraPlayerUIPrefab, playersUI.transform);
                plrUI.transform.position = plrUI.transform.position + new Vector3(0, -75 * i, 0);
                netPlr.uiAdjust = plrUI.GetComponent<PlayerUIInfo>();
                i++;
            }

            Debug.Log(netPlr.username);
            netPlr.healthChanged(100, netPlr.hp);
            netPlr.uiAdjust.username.text = netPlr.username;
            //netPlr.RpcMoney(netPlr.money);
        }
    }

    public void roundChanged(float oldValue, float newValue)
    {
        roundText.text = newValue.ToString();
    }

    // Game Logic
    float waitToDeal = -1;
    [Server]
    private void startRound()
    {
        Debug.Log("Starting Round");
        Debug.Log("Testing 1");
        NetworkPlayer tesP = allPlayers[0].gameObject.GetComponent<NetworkPlayer>();
        tesP.incrementMoney(-50);
        tesP.hp -= 30;
        waitToDeal = Time.unscaledTime + 2.5f;
    }

    private void Update()
    {
        if (waitToDeal != -1)
        {
            if (Time.unscaledTime >= waitToDeal)
            {
                Debug.Log("Testing 2");
                waitToDeal = -1;
                NetworkPlayer tesP = allPlayers[0].gameObject.GetComponent<NetworkPlayer>();
                tesP.incrementMoney(20);
                tesP.hp -= 10;
            }
        }
    }
}
