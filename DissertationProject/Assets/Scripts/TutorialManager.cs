using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    /*Steps:
     * 1: Check player can place basic tower
     * Spawn basic enemies slowly
     * 2: Check player can place fast shooter (maybe turn of other tower?)
     * Spawn fast enemies
     * 3: Check player can place cannon (maybe turn off fast shooter)
     * Spawn a group of enemies
    */

    Tower placedTower = null;
    public GameObject[] items;
    public BuildPadTutorial[] buildPads;
    int sectionID = 0;
    int textID = 0;
    bool shouldUpdateText = false;
    public TextMeshProUGUI textBox;
    public EnemySpawner spawner;
    public GameObject nextLevelButton;
    // Start is called before the first frame update

    private void Start()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            placedTower.type = Tower.towerType.Cannon;
            Debug.Log("As running on Web faking the tower type!");
        }

        if(Application.platform == RuntimePlatform.Android)
        {
            textBox.text = "Welcome to the tutorial level. Firstly press on one of the squares and build a tower. Press the settings button at the top any time to see in game options.";
        }
    }

    void Update()
    {
        switch (sectionID)
        {
            case 0:
                checkStepOne();
                break;
            case 1:
                checkStepTwo();
                break;
            case 2:
                checkStepThree();
                break;
            case 3:
                //Debug.Log("Tutorial finished");
                break;
            default:
                break;
        }
    }

    //This waits for the player to place a basic tower
    //We need to make sure that the player can't spawn any other towers than the basic one
    bool checkStepOne()
    {
        if(placedTower.type == Tower.towerType.Standard)
        {
            //Debug.Log("placed tower type: " + placedTower.type);
            items[0].SetActive(false);
            items[1].SetActive(true);
            //spawner.gameObject.SetActive(true);
            spawner.bInbetweenWaves = false;
            textID++;
            placedTower = null;
            turnOffAllBuildPads();
            updateText();
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkStepTwo()
    {
        if (placedTower.type == Tower.towerType.Fast)
        {
            items[1].SetActive(false);
            items[2].SetActive(true);
            //spawner.gameObject.SetActive(true);
            spawner.bInbetweenWaves = false;
            textID++;
            placedTower = null;
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                placedTower.type = Tower.towerType.Fast;
                Debug.Log("As running on Web faking the tower type!");
            }
            turnOffAllBuildPads();
            updateText();
            return true;
        }
        else
        {
            return false;
        } 
    }

    bool checkStepThree()
    {
        if (placedTower.type == Tower.towerType.Cannon)
        {
            items[2].SetActive(false);
            //spawner.gameObject.SetActive(true);
            spawner.bInbetweenWaves = false;
            textID++;
            placedTower = null;
            turnOffAllBuildPads();
            updateText();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setTower(ref Tower newTower)
    {
        placedTower = newTower;
    }

    void updateText()
    {
        switch (textID)
        {
            case 1:
                textBox.text = "Great! Now we'll spawn some enemies. Notice how the tower attacks them. This is the standard tower, it's fire speed isn't high nor is it's damage but it is cheap.";
                shouldUpdateText = true;
                StartCoroutine(countDown(5f));
                sectionID++;
                break;
            case 2:
                textBox.text = "Now buld a fast tower";
                GameObject towerToDestroy = GameObject.FindGameObjectWithTag("Tower");
                Destroy(towerToDestroy);
                break;
            case 3:
                textBox.text = "Fanstatic! Let's spawn some enemies again. Notice how this tower fires alot faster. Although it's damage is lower per shot.";
                shouldUpdateText = true;
                StartCoroutine(countDown(5f));
                sectionID++;
                break;
            case 4:
                textBox.text = "Finally build a cannon.";
                GameObject towerToDestroyAgain = GameObject.FindGameObjectWithTag("Tower");
                Destroy(towerToDestroyAgain);
                break;
            case 5:
                textBox.text = "Well done! Here's the final set of enemies. The cannon is expensive and not the fastest but deals lot's of damage and even damage enemies within a small radius.";
                shouldUpdateText = true;
                StartCoroutine(countDown(5f));
                sectionID++;
                break;
            case 6:
                textBox.text = "That's all for the tutorial. You can also click on a tower and press delete to destroy the tower and get the money back." +
                    "Click the button on screen to move to the actual game when your ready.";
                nextLevelButton.SetActive(true);
                break;
            default:
                break;
        }
    }

    void turnOffAllBuildPads()
    {
        for (int i = 0; i < buildPads.Length; i++)
        {
            if(buildPads[i] != null)
            {
                buildPads[i].setCanOpenBuildMenu(false);
            }
        }
    }

    void turnOnAllBuildPads()
    {
        for (int i = 0; i < buildPads.Length; i++)
        {
            if(buildPads[i] != null)
            {
                buildPads[i].setCanOpenBuildMenu(true);
            }
        }
    }

    IEnumerator countDown(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        //spawner.gameObject.SetActive(false);
        spawner.bInbetweenWaves = true;
        //Debug.Log("Set inBetweenWaves to true");
        //This effectively acts as a call back for the update text function
        if(shouldUpdateText == true)
        {
            shouldUpdateText = false;
            textID++;
            turnOnAllBuildPads();
            updateText();
        }
    }
}
