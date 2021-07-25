using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct WaveGroup {
    public string prefabName;
    public int count;
    public float pad; // time to wait until next unit group is spawned
    public float stagger; // time between spawning units of the same wave

    public NetworkPlayer target; // set to instance when only spawning for one player
}

public class WaveManager : NetworkBehaviour
{
    public static WaveManager Global;

    public Queue<WaveGroup> waveQueue = new Queue<WaveGroup>();
    public bool isWaveOngoing = false;
    public PathNode[] spawnPointList;
    public NetworkPlayer[] coresPlayers;

    // config
    public float spawnRadius = 0.25f;

    [Server]
    void Start()
    {
        WaveManager.Global = this;
    }

    [Server]
    public void startWave() {
        if (WaveManager.Global.isWaveOngoing) {
            Debug.LogError("There is already a wave ongoing.");
            return;
        }
        StartCoroutine(spawnUnitWave());
    }

    [Server]
    private IEnumerator spawnUnitWave() {

        WaveManager.Global.isWaveOngoing = true;

        while (WaveManager.Global.waveQueue.Count > 0) {

            WaveGroup waveInfo = WaveManager.Global.waveQueue.Dequeue();

            StartCoroutine(spawnUnits(waveInfo));

            yield return new WaitForSeconds(waveInfo.pad);
        }

        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        WaveManager.Global.isWaveOngoing = false;
    }

    [Server]
    private IEnumerator spawnUnits(WaveGroup waveInfo) {

        for (int i = 0; i < waveInfo.count; i++) {

            // create enemy gameobjects and set them up
            int m = 0;
            foreach (PathNode p in WaveManager.Global.spawnPointList) {
                if (waveInfo.target == null || waveInfo.target == coresPlayers[m])
                {
                    Vector3 pos = p.transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
                    GameObject newEnemy = (GameObject)Instantiate(Resources.Load(waveInfo.prefabName), pos, Quaternion.identity);
                    NetworkServer.Spawn(newEnemy);

                    PathTraveller travelCode = newEnemy.GetComponent<PathTraveller>();
                    travelCode.destinationNode = p;
                    travelCode.attacking = coresPlayers[m];
                }
                
                m++;
            }

            yield return new WaitForSeconds(waveInfo.stagger);
        }
    }

    [Server]
    public void queueUnit(string prefabName, int count, float pad, float stagger) {
        WaveManager.Global.waveQueue.Enqueue(new WaveGroup {prefabName=prefabName, count=count, pad=pad, stagger=stagger});
    }

    [Server]
    public void spawnUnit(string prefabName, int count, float stagger) {
        if (!WaveManager.Global.isWaveOngoing) {
            Debug.LogError("There is no ongoing wave.");
            return;
        }
        StartCoroutine(spawnUnits(new WaveGroup {prefabName=prefabName, count=count, pad=0, stagger=stagger}));
   }

    [Server]
    public void spawnUnit(string prefabName, int count, float stagger, NetworkPlayer target)
    {
        if (!WaveManager.Global.isWaveOngoing)
        {
            Debug.LogError("There is no ongoing wave.");
            return;
        }
        StartCoroutine(spawnUnits(new WaveGroup { prefabName = prefabName, count = count, pad = 0, stagger = stagger, target = target }));
    }
}
