using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    EnemySpawner enemySpawner;
    public Enemy[] enemies;
    List<Tower> towers;
    ScoreManager scoreManager;

    //DEBUG COMMANDS
    public bool shouldDeleteTowers = false;
    public bool shouldDecrementHealth = false;
    public bool shouldDecrementMoney = false;
    private void Start()
    {
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        towers = new List<Tower>();
    }

    public void spawnNewWave()
    {

        //TODO: Decided if this should be a list or stay as an array
        //Since the AI will be creating waves etc probably best to have something
        //That can be adjusted dynamically
        EnemySpawner.WaveComponent waveComponent = new EnemySpawner.WaveComponent();
        EnemySpawner.WaveComponent[] waveComponentArray = new EnemySpawner.WaveComponent[1];
        int enemyToUse = Random.Range(0, enemies.Length);
        waveComponent.enemyPrefab = enemies[enemyToUse].gameObject;

        waveComponent.num = Random.Range(1,10);

        waveComponentArray[0] = waveComponent;

        scoreManager.incrementWaveCounter();
        enemySpawner.createWave(waveComponentArray);

        cheat();
    }

    //This function will randomly cheat when a new wave is created
    void cheat()
    {
        int randomNumber = 0;
        if (shouldDeleteTowers == true)
        {
            randomNumber = Random.Range(0, 11);
            if (randomNumber == 0)
            {
                destroyTower();
                return;
            }
        }

        if (shouldDecrementHealth == true)
        {
            randomNumber = Random.Range(0, 1);
            if (randomNumber == 0)
            {
                subtractPlayerHealth();
                return;
            }
        }

        if (shouldDecrementMoney == true)
        {
            randomNumber = Random.Range(0, 1);
            if (randomNumber == 0)
            {
                subtractPlayerMoney();
                return;
            }
        }
    }

    public void addTower(Tower newTower)
    {
        towers.Add(newTower);
    }

    public int getTowersCount()
    {
        return towers.Count;
    }

    void destroyTower()
    {
        int randomNumber = Random.Range(0, towers.Count);
        Destroy(towers[randomNumber].gameObject);

        for (int i = 0; i < towers.Count; i++)
        {
            if(towers[i] == null)
            {
                towers.RemoveAt(i);
            }
        }

    }

    public void subtractPlayerHealth()
    {
        scoreManager.decrementLife();
    }

    public void subtractPlayerMoney()
    {
        scoreManager.decrementMoney(5);
    }
}