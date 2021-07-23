using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WaveGroup {
    public string prefabName;
    public int count;
    public float pad; // time to wait until next unit group is spawned
    public float stagger; // time between spawning units of the same wave
}

public class WaveManager : MonoBehaviour
{
    public static Queue<WaveGroup> waveQueue = new Queue<WaveGroup>();

    public void startWave() {
        if (!WaveManager.isWaveOnGoing()) {
            Debug.LogError("There is already a wave ongoing.");
            return;
        }
        StartCoroutine(spawnUnitWave());
    }

    private IEnumerator spawnUnitWave() {

        while (WaveManager.isWaveOnGoing()) {
            WaveGroup waveInfo = WaveManager.waveQueue.Dequeue();

            StartCoroutine(spawnUnits(waveInfo));

            yield return new WaitForSeconds(waveInfo.pad);
        }
    }

    private IEnumerator spawnUnits(WaveGroup waveInfo) {

        for (int i = 0; i < waveInfo.count; i++) {

            // create enemy gameobjects and set them up
            // GameObject newEnemy = Instantiate();

            yield return new WaitForSeconds(waveInfo.stagger);
        }
    }

    public static bool isWaveOnGoing() {
        return WaveManager.waveQueue.Count > 0;
    }

    public static void queueUnit(string prefabName, int count, float pad, float stagger) {
        WaveManager.waveQueue.Enqueue(new WaveGroup {prefabName=prefabName, count=count, pad=pad, stagger=stagger});
    }

    public static void spawnUnit<T>(int count) {

   }

}
