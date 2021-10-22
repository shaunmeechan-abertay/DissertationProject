using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float spawnCooldown = 1.0f;
    float spawnCooldownleft = 0.0f;
    int waveCounter = 0;
    int groupCounter = 0;
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
        public WaveComponent[] waveComponent;
        public List<WaveComponent> waveComponents = new List<WaveComponent>();
    }

    public Waves[] waves;
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
        if(spawnCooldownleft < 0 && waveCounter <= waves.Length)
        {
            spawnCooldownleft = spawnCooldown;

            //Go through the wave until we find something to spawn
            //foreach (WaveComponent wc in waveComps)
            //{
            //    if (wc.spawned < wc.num)
            //    {
            //        //Spawn it
            //        Instantiate(wc.enemyPrefab, transform.position, transform.rotation);
            //        wc.spawned++;
            //        spawned = true;
            //        break;
            //    }
            //}

            for (int i = groupCounter; i < waves[waveCounter].waveComponent.Length; i++)
            {
                //Go through the component and spawn the enemies in it
                if(waves[waveCounter].waveComponent[i].spawned < waves[waveCounter].waveComponent[i].num)
                {
                        //Spawn it
                        Instantiate(waves[waveCounter].waveComponent[i].enemyPrefab, transform.position, transform.rotation);
                        waves[waveCounter].waveComponent[i].spawned++;
                        break;
                }
                else
                {
                    groupCounter++;
                    //break;
                }
            }

            //Check to see if this wave finished
            if(groupCounter >= waves[waveCounter].waveComponent.Length)
            {
                Debug.Log("Wave finished!");
                increaseWaveCounter();
                groupCounter = 0;
            }
        }
    }

    void increaseWaveCounter()
    {
        waveCounter++;
        if(waveCounter > waves.Length - 1)
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
        Debug.Log("Received:" + waveComponents);
        //THIS IS BAD, DO NOT DO!
        Waves newWave = new Waves();
        newWave.waveComponents.Add(waveComponents[0]);
        //newWave.waveComponent[0] = waveComponents[0];
        wavesList.Add(newWave);
    }

}
