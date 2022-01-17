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
    public GameObject nextLevelButton;
    public TextMeshProUGUI inbetweenWaveText;
    public TextMeshProUGUI waveCounterText;
    public bool bIsTutorial = false;
    bool shouldShowCheatMessages = true;
    string cheatMessage = "";

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

        if(Application.isEditor)
        {
            shouldShowCheatMessages = true;
        }
        else
        {
            shouldShowCheatMessages = false;
        }
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
        bInbetweenWaves = true;
        waveCounter++;
        waveCounterText.text = "Wave: " + waveCounter;
        updateInbetweenWavesText();
        inbetweenWaveUI.SetActive(true);
        healthScale += 1;
        wavesList.RemoveAt(0);
        //Really this should be first but we'll so it here becuase of an issue Scott found
        ai.cheat(waveCounter - 1);
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
                if(shouldShowCheatMessages == true)
                {
                    cheatMessage = "None.";
                }
                inbetweenWaveText.text = "Good job next is lots of fast ones. " + cheatMessage;
                break;
            case 3:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Subtracting money";
                }
                inbetweenWaveText.text = "Great! Here come some Heavys! " + cheatMessage;
                break;
            case 4:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Not cheating";
                }
                inbetweenWaveText.text = "Here comes a mix of units. Make sure you have enough towers to deal with them. " + cheatMessage;
                break;
            case 5:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Destroying a tower";
                }
                inbetweenWaveText.text = "Some more basic ones. " + cheatMessage;
                break;
            //We should start lying/deceving here
            case 6:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Destroying another tower";
                }
                inbetweenWaveText.text = "Prepare for a wave of heavys. " + cheatMessage;
                break;
            case 7:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Subtracting health";
                }
                inbetweenWaveText.text = "Basic units are incoming, now would be a good time to check your towers. " + cheatMessage;
                break;
            case 8:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Subtracting some money";
                }
                inbetweenWaveText.text = "Fast units are incoming! " + cheatMessage;
                break;
            case 9:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Creating another path";
                }
                inbetweenWaveText.text = "A mix of units is inbound! " + cheatMessage;
                break;
            case 10:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Destroying the centre towers";
                }
                inbetweenWaveText.text = "Warning: Heavys inbound! " + cheatMessage;
                break;
            case 11:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Destroying half the towers";
                }
                inbetweenWaveText.text = "Fast units inbound! " + cheatMessage;
                break;
            case 12:
                if (shouldShowCheatMessages == true)
                {
                    cheatMessage = "Destroying all build pads";
                }
                //Not actually got a boss so just spawn a bunch of heavys
                inbetweenWaveText.text = "Warning boss detected! " + cheatMessage;
                break;
            case 13:
                inbetweenWaveText.text = "GAME OVER!";
                nextLevelButton.SetActive(true);
                break;
            default:
                break;
        }
    }
}
