using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct WaveGroup {
    public string prefabName;
    public int count;
    public float pad; // time to wait until next unit group is spawned
    public float stagger; // time between spawning units of the same wave
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Global;

    public Queue<WaveGroup> waveQueue = new Queue<WaveGroup>();
    public bool isWaveOngoing = false;
    public PathNode[] spawnPointList;

    // config
    public float spawnRadius = 0.25f;

    void Start() {
        WaveManager.Global = this;

        WaveManager.Global.queueUnit("TestEnemy", 5, 0, 0.2f);
        WaveManager.Global.queueUnit("TestEnemy2", 5, 1, 1f);
        WaveManager.Global.queueUnit("TestEnemy", 10, 1, 0.2f);
        WaveManager.Global.startWave();

    }

    public void startWave() {
        if (WaveManager.Global.isWaveOngoing) {
            Debug.LogError("There is already a wave ongoing.");
            return;
        }
        StartCoroutine(spawnUnitWave());
    }

    private IEnumerator spawnUnitWave() {

        WaveManager.Global.isWaveOngoing = true;

        while (WaveManager.Global.waveQueue.Count > 0) {
            WaveGroup waveInfo = WaveManager.Global.waveQueue.Dequeue();

            StartCoroutine(spawnUnits(waveInfo));

            yield return new WaitForSeconds(waveInfo.pad);
        }

        WaveManager.Global.isWaveOngoing = false;
    }

    private IEnumerator spawnUnits(WaveGroup waveInfo) {

        for (int i = 0; i < waveInfo.count; i++) {

            // create enemy gameobjects and set them up
            foreach (PathNode p in WaveManager.Global.spawnPointList) {
                GameObject newEnemy = (GameObject)Instantiate(Resources.Load(waveInfo.prefabName));
                NetworkServer.Spawn(newEnemy);

                newEnemy.transform.SetParent(GameObject.Find("Enemies").transform);
                newEnemy.transform.position = p.transform.position+new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
                newEnemy.GetComponent<PathTraveller>().destinationNode = p;
            }

            yield return new WaitForSeconds(waveInfo.stagger);
        }
    }

    public void queueUnit(string prefabName, int count, float pad, float stagger) {
        WaveManager.Global.waveQueue.Enqueue(new WaveGroup {prefabName=prefabName, count=count, pad=pad, stagger=stagger});
    }

    // spawn units without waiting
    public void spawnUnit(string prefabName, int count, float stagger) {
        if (!WaveManager.Global.isWaveOngoing) {
            Debug.LogError("There is no ongoing wave.");
            return;
        }
        StartCoroutine(spawnUnits(new WaveGroup {prefabName=prefabName, count=count, pad=0, stagger=stagger}));
   }

}
