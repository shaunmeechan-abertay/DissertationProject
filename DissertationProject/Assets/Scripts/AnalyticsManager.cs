//Good reference for this stuff is: https://youtu.be/3jDD-E1OUkc

using UnityEngine;
using UnityEngine.Analytics;
using System;

public class AnalyticsManager : MonoBehaviour
{
    GUIDHelper gUIDHelper;
    string UID;

    private void Start()
    {
        gUIDHelper = GameObject.FindObjectOfType<GUIDHelper>();
    }
    public void sendWinEvent()
    {
        //Check if the UID is null
        if(UID == null)
        {
            getUID();
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelWin: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    public void sendLoseEvent()
    {
        if (UID == null)
        {
            getUID();
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelLose: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    public void sendCheatHealthEvent()
    {
        if(UID == null)
        {
            getUID();
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by decreasing health: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }
    
    public void sendCheatMoneyEvent()
    {
        if(UID == null)
        {
            getUID();
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by decreasing money: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }
    
    public void sendCheatDestoryTowerEvent()
    {
        if(UID == null)
        {
            getUID();
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by destroying a tower: " + UID);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    public void sendCheatDestoryCentreEvent()
    {
        if(UID == null)
        {
            getUID();
        }

        AnalyticsResult analyticsResult = Analytics.CustomEvent("AI cheated by destroying the centre towers: " + UID);
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
}
