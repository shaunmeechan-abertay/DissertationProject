using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float spawnCooldown = 1.0f;
    float spawnCooldownleft = 0.0f;
    int waveCounter = 1;
    int groupCounter = 0;
    public float healthScale = 0.5f;
    public bool bInbetweenWaves = true;
    public GameObject inbetweenWaveUI;
    public TextMeshProUGUI inbetweenWaveText;
    public TextMeshProUGUI waveCounterText;
    public bool bIsTutorial = false;

    //AnalyticsManager analyticsManager;
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
        //analyticsManager = GameObject.FindObjectOfType<AnalyticsManager>();
        ai = GameObject.FindObjectOfType<AI>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldownleft -= Time.deltaTime;
        Waves currentWave = wavesList[0];

        if(spawnCooldownleft < 0 && wavesList.Count != 0 && bInbetweenWaves == false)
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
                if(bIsTutorial == true)
                {
                    increaseWaveCounterTutorial();
                }
                else
                {
                    increaseWaveCounter();
                }
            }
        }
    }

    void increaseWaveCounter()
    {
        ai.cheat(waveCounter);
        bInbetweenWaves = true;
        waveCounter++;
        waveCounterText.text = "Wave: " + waveCounter;
        updateInbetweenWavesText();
        inbetweenWaveUI.SetActive(true);
        healthScale += 1;
        wavesList.RemoveAt(0);
        if(waveCounter > wavesList.Count - 1)
        {
            Debug.Log("Ran out of wave. Adding another...");
            //ai.spawnNewWave();
        }
    }

    void increaseWaveCounterTutorial()
    {
        if(ai != null)
        {
            ai.cheat(waveCounter);
        }
        bInbetweenWaves = true;
        waveCounter++;
        waveCounterText.text = "Wave: " + waveCounter;
        wavesList.RemoveAt(0);
        if(waveCounter > wavesList.Count - 1)
        {
            Debug.Log("Ran out of wave. Adding another...");
            //ai.spawnNewWave();
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

    public void setBInbetweenWaves(bool value)
    {
        bInbetweenWaves = value;
    }

    void updateInbetweenWavesText()
    {
        switch (waveCounter)
        {
            case 1:
                inbetweenWaveText.text = "Hello world!";
                break;
            case 2:
                inbetweenWaveText.text = "Good job, next are lot's of fast ones. No cheat this time";
                break;
            case 3:
                inbetweenWaveText.text = "Great! Here come some Heavys! Subtracting money";
                break;
            case 4:
                inbetweenWaveText.text = "Here comes a mix of units. Not cheating";
                break;
            case 5:
                inbetweenWaveText.text = "Some more basic ones. Destroying a tower";
                break;
            //We should start lying/deceving here
            case 6:
                inbetweenWaveText.text = "Prepare for a wave of heavys. Destroying another tower";
                break;
            case 7:
                inbetweenWaveText.text = "Subtracting health";
                break;
            case 8:
                inbetweenWaveText.text = "Subtracting some money";
                break;
            case 9:
                inbetweenWaveText.text = "Creating another path";
                break;
            case 10:
                inbetweenWaveText.text = "Destroying the centre towers";
                break;
            case 11:
                inbetweenWaveText.text = "Destroying half the towers";
                break;
            case 12:
                inbetweenWaveText.text = "Destroying all build pads";
                break;
            default:
                break;
        }
    }
}
