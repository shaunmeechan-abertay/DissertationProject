using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float spawnCooldown = 1.0f;
    float spawnCooldownleft = 0.0f;
    int waveCounter = 0;
    int groupCounter = 0;
    int healthScale = 1;

    AnalyticsManager analyticsManager;
    AI ai;
    [System.Serializable]
    public class WaveComponent{
        public GameObject enemyPrefab = null;
        public int num = 0;
        [System.NonSerialized]
        public int spawned = 0;
    }    
    
    [System.Serializable]
    public class Waves{
        public List<WaveComponent> waveComponents = new List<WaveComponent>();
    }

    public List<Waves> wavesList;
    // Start is called before the first frame update
    void Start()
    {
        analyticsManager = GameObject.FindObjectOfType<AnalyticsManager>();
        ai = GameObject.FindObjectOfType<AI>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldownleft -= Time.deltaTime;
        Waves currentWave = wavesList[0];

        if(spawnCooldownleft < 0 && wavesList.Count != 0)
        {
            spawnCooldownleft = spawnCooldown;

            for (int i = groupCounter; i < currentWave.waveComponents.Count; i++)
            {
                //Go through the component and spawn the enemies in it
                if(currentWave.waveComponents[i].spawned < currentWave.waveComponents[i].num)
                {
                    //Spawn it
                    GameObject temp = Instantiate(currentWave.waveComponents[i].enemyPrefab, transform.position, transform.rotation);
                    temp.GetComponent<Enemy>().health *= healthScale;
                    currentWave.waveComponents[i].spawned++;
                    break;
                }
                else
                {
                    groupCounter++;
                }
            }

            //Check to see if this wave finished
            if(groupCounter >= currentWave.waveComponents.Count)
            {
                Debug.Log("Wave finished!");
                groupCounter = 0;
                increaseWaveCounter();
            }
        }
    }

    void increaseWaveCounter()
    {
        waveCounter++;
        wavesList.RemoveAt(0);
        if(waveCounter > wavesList.Count - 1)
        {
            Debug.Log("Ran out of wave. Adding another...");
            ai.spawnNewWave();
            //analyticsManager.sendWinEvent();
            //Destroy(gameObject);
        }
    }

    public void createWave(WaveComponent[] waveComponents)
    {
        Debug.Log("create wave was called!");

        Waves newWave = new Waves();
        for (int i = 0; i < waveComponents.Length; i++)
        {
            newWave.waveComponents.Add(waveComponents[i]);
        }
        wavesList.Add(newWave);
        healthScale += 1;
    }
}
