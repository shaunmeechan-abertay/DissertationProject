//Good reference for this stuff is: https://youtu.be/3jDD-E1OUkc

using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    GUIDHelper gUIDHelper;
    string UID;
    public bool bShouldSendAnalytics = true;
    int towerCount = 0;
    int basicTowerCount = 0;
    int fastTowerCount = 0;
    int cannonTowerCount = 0;

    private void Start()
    {
        gUIDHelper = GameObject.FindObjectOfType<GUIDHelper>();
    }
    public void sendWinEvent()
    {
        if(bShouldSendAnalytics == false)
        {
            return;
        }
        //Check if the UID is null
        if(UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if(UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelWin: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player lives: " + GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>().lives + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player cash: " + GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>().money + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player tower count: " + towerCount + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player basic tower count: " + basicTowerCount + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player fast tower count: " + fastTowerCount + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player cannon tower count: " + cannonTowerCount + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);

    }

    public void sendLoseEvent()
    {
        if (bShouldSendAnalytics == false)
        {
            return;
        }

        if (UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if (UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelLose on wave " + GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().getWave() + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player cash: " + GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>().money + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player tower count: " + towerCount + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player basic tower count: " + basicTowerCount + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player fast tower count: " + fastTowerCount + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
        analyticsResult = Analytics.CustomEvent("Player cannon tower count: " + cannonTowerCount + " : " + UID);
        Debug.Log("Analytics result: " + analyticsResult);


        //We could also send how many towers they had
        //Maybe also how many they had over the whole game
    }

    public void sendCheatHealthEvent()
    {
        if (bShouldSendAnalytics == false)
        {
            return;
        }

        if (UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if (UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by decreasing health: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }
    
    public void sendCheatMoneyEvent()
    {
        if (bShouldSendAnalytics == false)
        {
            return;
        }

        if (UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if (UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by decreasing money: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }
    
    public void sendCheatDestoryTowerEvent()
    {
        if (bShouldSendAnalytics == false)
        {
            return;
        }

        if (UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if (UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by destroying a tower: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    public void sendCheatDestoryCentreEvent()
    {
        if (bShouldSendAnalytics == false)
        {
            return;
        }

        if (UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if (UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by destroying the centre towers: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    public void sendCheatCreatedNewPathEvent()
    {
        if (bShouldSendAnalytics == false)
        {
            return;
        }

        if (UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if (UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by creating a new path: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    public void sendCheatDestroyedAllBuildPads()
    {
        if (bShouldSendAnalytics == false)
        {
            return;
        }

        if (UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if (UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by Destroying all Buildpads: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    public void sendCheatDestroyedHalfOfTowers()
    {
        if (bShouldSendAnalytics == false)
        {
            return;
        }

        if (UID == null)
        {
            getUID();
        }

        //If the UID is still null something has gone very wrong
        //Error out!
        if (UID == null)
        {
            Debug.LogError("ERROR: Unable to get GUID");
            return;
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by Destroying half of the towers: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    void getUID()
    {
       UID = gUIDHelper.getUIDAsString();
       //If it is still null something has gone very wrong!
       if (UID == null)
       {
            Debug.LogError("ERROR: UID is null!");
            return;
       }
    }

    public void incrementTowerCount()
    {
        towerCount++;
    }

    public void incrementBasicTowerCount()
    {
        basicTowerCount++;
    }

    public void incrementFastTowerCount()
    {
        fastTowerCount++;
    }

    public void incrementCannonTowerCount()
    {
        cannonTowerCount++;
    }

}
