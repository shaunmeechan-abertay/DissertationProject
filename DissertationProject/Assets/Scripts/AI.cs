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
        //TODO: Decided if this should be a list or stay as an array
        //Since the AI will be creating waves etc probably best to have something
        //That can be adjusted dynamically
        EnemySpawner.WaveComponent waveComponent = new EnemySpawner.WaveComponent();
        EnemySpawner.WaveComponent[] waveComponentArray = new EnemySpawner.WaveComponent[1];
        waveComponent.enemyPrefab = testEnemy;
        waveComponent.num = Random.Range(1,10);

        waveComponentArray[0] = waveComponent;

        //wave[0].waveComponent = waveComponent;
        enemySpawner.createWave(waveComponentArray);
    }
}