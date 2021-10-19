using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float spawnCooldown = 1.0f;
    float spawnCooldownleft = 0.0f;
    int waveCounter = 0;
    int groupCounter = 0;
    [System.Serializable]
    public class WaveComponent{
        public GameObject enemyPrefab;
        public int num;
        [System.NonSerialized]
        public int spawned = 0;
    }    
    
    [System.Serializable]
    public class Waves{
        public WaveComponent[] waveComponent;
    }

    public WaveComponent[] waveComps;
    public Waves[] waves;
    // Start is called before the first frame update
    void Start()
    {
        //spawnWave(waves[0]);
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldownleft -= Time.deltaTime;
        if(spawnCooldownleft < 0)
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
            Destroy(gameObject);
        }
    }

}
