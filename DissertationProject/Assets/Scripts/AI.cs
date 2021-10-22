using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    EnemySpawner enemySpawner;
    public GameObject testEnemy;

    private void Start()
    {
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
    }

    public void spawnNewWave()
    {
        //EnemySpawner.Waves[] wave = new EnemySpawner.Waves[1];
        EnemySpawner.WaveComponent waveComponent = new EnemySpawner.WaveComponent();
        EnemySpawner.WaveComponent[] waveComponentArray = new EnemySpawner.WaveComponent[1];
        //EnemySpawner.WaveComponent waveComponent1 = new EnemySpawner.WaveComponent();
        waveComponent.enemyPrefab = testEnemy;
        waveComponent.num = 2;

        waveComponentArray[0] = waveComponent;

        //wave[0].waveComponent = waveComponent;
        enemySpawner.createWave(waveComponentArray);
    }
}