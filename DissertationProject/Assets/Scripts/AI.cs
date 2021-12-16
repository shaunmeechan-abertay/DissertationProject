using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    EnemySpawner enemySpawner;
    public Enemy[] enemies;
    List<Tower> towers;
    List<Tower> centreTowers;
    List<Tower> towersInPath;
    ScoreManager scoreManager;
    AnalyticsManager analyticsManager;
    public GameObject originalPath;
    public GameObject cheatedPath;
    public GameObject[] destroyableGroundSprites;
    public GameObject[] destroyableBuildPads;
    public bool bCanPlayAudio = true;
    //DEBUG COMMANDS - Should be true so the AI can cheat in normal gameplay
    //but in editor can be changed as needed
    public bool shouldDeleteTowers = true;
    public bool shouldDecrementHealth = true;
    public bool shouldDecrementMoney = true;
    public bool shouldDestoryCentreTowers = true;
    public bool shouldCreateNewPath = true;

    //Start at 3 as the first 2 waves are defined in the inspector
    //int waveCounter = 1;

    bool bHasDestroyedCentreTowers = false;
    private void Start()
    {
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        analyticsManager = GameObject.FindObjectOfType<AnalyticsManager>();
        towers = new List<Tower>();
        centreTowers = new List<Tower>();
        towersInPath = new List<Tower>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            bCanPlayAudio = !bCanPlayAudio;
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].bCanPlayAudio = bCanPlayAudio;
            }

            for (int i = 0; i < centreTowers.Count; i++)
            {
                centreTowers[i].bCanPlayAudio = bCanPlayAudio;
            }

            for (int i = 0; i < towersInPath.Count; i++)
            {
                towersInPath[i].bCanPlayAudio = bCanPlayAudio;
            }
        }
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

        waveComponent.num = Random.Range(1,8);

        waveComponentArray[0] = waveComponent;

        scoreManager.incrementWaveCounter();
        enemySpawner.createWave(waveComponentArray);

        //waveCounter++;

        //cheat();
    }

    //This function will randomly cheat when a new wave is created
    public void cheat(int waveCounter)
    {
        switch (waveCounter)
        {
            case 3:
                subtractPlayerMoney();
                break;
            case 4:
                break;
            case 5:
                destroyTower();
                break;
            case 6:
                destroyTower();
                break;
            case 7:
                subtractPlayerHealth();
                break;
            case 8:
                subtractPlayerMoney();
                break;
            case 9:
                createNewPath();
                break;
            case 10:
                destroyCentreTowers();
                break;
            case 11:
                destroyTowers(0.5f);
                break;
            case 12:
                destroyAllBuildPads();
                break;
            default:
                //Debug.LogError("ERROR: waveCounter was not valid for switch statement.");
                break;
        }
    }

    public void addTower(Tower newTower)
    {
        towers.Add(newTower);
    }

    public void addCentreTower(Tower newTower)
    {
        centreTowers.Add(newTower);
    }

    public void addTowersInPath(Tower newTower)
    {
        towersInPath.Add(newTower);
    }

    public int getTowersCount()
    {
        return towers.Count;
    }

    void destroyTower()
    {
        if(towers.Count == 0)
        {
            return;
        }
        int randomNumber = Random.Range(0, towers.Count);
        towers[randomNumber].destroy();
        //Destroy(towers[randomNumber].gameObject);

        for (int i = 0; i < towers.Count; i++)
        {
            if(towers[i] == null)
            {
                towers.RemoveAt(i);
            }
        }

    }

    void destroyAllBuildPads()
    {
        for (int i = 0; i < destroyableBuildPads.Length; i++)
        {
            Destroy(destroyableBuildPads[i]);
        }

        analyticsManager.sendCheatDestroyedAllBuildPads();
    }

    //Function to destroy x towers.
    //Use 0.5f to destroy the first half of the towers placed
    void destroyTowers(float count)
    {
        Tower[] towers = GameObject.FindObjectsOfType<Tower>();
        if(count == 0.5f)
        {
            for (int i = 0; i < towers.Length/2; i++)
            {
                towers[i].destroy();
                //Destroy(towers[i].gameObject);
            }
            analyticsManager.sendCheatDestroyedHalfOfTowers();
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                if(towers[i] != null)
                {
                    towers[i].destroy();
                    //Destroy(towers[i].gameObject);
                }
                else
                {
                    break;
                }
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

    void destroyCentreTowers()
    {
        for (int i = 0; i < centreTowers.Count; i++)
        {
            centreTowers[i].destroy();
            //Destroy(centreTowers[i].gameObject);
        }
        centreTowers.Clear();
        bHasDestroyedCentreTowers = true;
        analyticsManager.sendCheatDestoryCentreEvent();
    }

    void createNewPath()
    {
        Destroy(originalPath);
        cheatedPath.name = "Path";
        for (int i = 0; i < destroyableGroundSprites.Length; i++)
        {
            if (destroyableGroundSprites[i] != null)
            {
                Destroy(destroyableGroundSprites[i]);
            }
        }

        Destroy(destroyableBuildPads[0]);
        Destroy(destroyableBuildPads[1]);

        for (int i = 0; i < towersInPath.Count; i++)
        {
            towersInPath[i].destroy();
            //Destroy(towersInPath[i].gameObject);
        }
        towersInPath.Clear();

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        analyticsManager.sendCheatCreatedNewPathEvent();
    }
}