//Good reference for this stuff is: https://youtu.be/3jDD-E1OUkc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{


    public void sendWinEvent()
    {
        int randomNumber = Random.Range(0, 100);
       AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelWin:" + randomNumber);
        Debug.Log("Analytics result: " + analyticsResult);
    }

    public void sendLoseEvent()
    {
        int randomNumber = Random.Range(0, 100);
       AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelLose:" + randomNumber);
        Debug.Log("Analytics result: " + analyticsResult);
    }


}
