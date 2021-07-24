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
    public WaveManager waveManager;
    public GameObject mainCamera;
    public GameObject playersUI;
    public GameObject mapSlots;
    public TMP_Text roundText;
    public float roundPrep;

    [SyncVar(hook = nameof(roundChanged))]
    public float round = 0.0f;

    [SyncVar(hook = nameof(roundChanged))]
    public float roundCountDown = -1f;

    float roundStart = -2f;

    // Server Init
    public override void onBegin(Dictionary<NetworkIdentity, string> players)
    {
        List<PathNode> nodePaths = new List<PathNode> { };
        allPlayers = new List<NetworkIdentity>();
        int c = 0;
        foreach (KeyValuePair<NetworkIdentity, string> plr in players)
        {
            NetworkPlayer netPlr = plr.Key.gameObject.GetComponent<NetworkPlayer>();
            Transform playArea = mapSlots.transform.GetChild(c);
            netPlr.username = plr.Value;
            netPlr.location = playArea.position;
            allPlayers.Add(plr.Key);

            nodePaths.Add(playArea.Find("pathroot").Find("pathnode0").GetComponent<PathNode>());
            c++;
        }

        RpcInitializeGame(allPlayers.ToArray());

        round = 1.0f;
        waveManager.spawnPointList = nodePaths.ToArray();
        roundStart = -1f;
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
        CameraController camControl = mainCamera.GetComponent<CameraController>();
        int i = 0;
        foreach (NetworkIdentity id in playerIDs)
        {
            NetworkPlayer netPlr = id.gameObject.GetComponent<NetworkPlayer>();
            if (id.isLocalPlayer)
            {
                netPlr.dataSingleton = singletonDataStorage;
                netPlr.uiAdjust = localPlayerUI;
                netPlr.locationChanged(new Vector3(), netPlr.location);

            }
            else
            { 
                // make new prefab
                GameObject plrUI = Instantiate(extraPlayerUIPrefab, playersUI.transform);
                plrUI.transform.position = plrUI.transform.position + new Vector3(0, -75 * i, 0);
                netPlr.uiAdjust = plrUI.GetComponent<PlayerUIInfo>();

                netPlr.uiAdjust.changeOrigin.onClick.AddListener(() => camControl.SetTarget(new Vector2(netPlr.location.x, netPlr.location.y)));
                i++;
            }

            netPlr.setClientMoney(netPlr.money);
            netPlr.healthChanged(100, netPlr.hp);
            netPlr.uiAdjust.username.text = netPlr.username;
        }
    }

    // Client listeners
    public void roundChanged(float oldValue, float newValue)
    {
        if (roundCountDown == -1)
        {
            roundText.text = round.ToString();
        }
        else
        {
            roundText.text = roundCountDown.ToString();
        }
    }

    public void wantPurchase(string towerName, Vector3 position)
    {
        CmdPurchase(towerName, position);
    }

    // Purchase command
    [Command(requiresAuthority = false)]
    public void CmdPurchase(string towerName, Vector3 position, NetworkConnectionToClient sender = null)
    {
        Debug.Log("Purchase attempt");
        NetworkIdentity id = sender.identity;
        GameObject towerpref = Resources.Load<GameObject>(towerName);

        if (towerpref == null)
        {
            Debug.Log("Rescource could not be found");
            return;
        }

        GameObject tower = Instantiate(towerpref, position, Quaternion.identity);
        int cost = tower.GetComponent<Tower>().data.cost;
        NetworkPlayer plr = id.gameObject.GetComponent<NetworkPlayer>();

        if (plr.money >= cost)
        {
            plr.incrementMoney(cost * -1);
            tower.AddComponent<TowerHoverHighlight>();
            tower.GetComponent<Tower>().placed = true;
            NetworkServer.Spawn(tower);
        }
        else
        {
            Destroy(tower);
        }
    }

    // Game Logic
    [Server]
    private void startRound()
    {
        Debug.Log("Starting Round");
        waveManager.queueUnit("TestEnemy", 5, 0, 0.2f);
        waveManager.queueUnit("TestEnemy2", 5, 1, 1f);
        waveManager.queueUnit("TestEnemy", 10, 1, 0.2f);
        waveManager.startWave();
        roundStart = Time.realtimeSinceStartup + roundPrep * 5;
    }

    private void Update()
    {
        // Game hasn't started yet
        if (roundStart == -2)
        {
            return;
        }

        // Start preparing for next round
        if (roundStart == -1 && !waveManager.isWaveOngoing)
        {
            roundStart = Time.realtimeSinceStartup + roundPrep;
            round += 0.1f;
        }

        // Start the round horde
        if (roundStart != -1 && Time.realtimeSinceStartup >= roundStart)
        {
            startRound();
            roundStart = -1;
        }

        // Adjust what the client will show
        if (roundStart == -1)
        {
            roundCountDown = -1;
        }
        else
        {
            roundCountDown = (int)(roundStart - Time.realtimeSinceStartup);
        }
    }
}
