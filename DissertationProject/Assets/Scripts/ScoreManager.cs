using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int money = 10;
    int lives = 2;
    int wave = 1;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI waveText;

    bool bIsBuildMenuOpen = false;
    //DEBUG
    public bool canLose = true;

    AnalyticsManager analyticsManager;
    private void Start()
    {
        moneyText.text = "Cash:" + money.ToString();
        livesText.text = "Lives:" + lives.ToString();
        waveText.text = "Wave:" + wave.ToString();
        analyticsManager = GameObject.FindObjectOfType<AnalyticsManager>();
    }
    public void decrementLife(int l = 1)
    {
        lives -= l;
        if(lives <= 0)
        {
            gameOver();
        }
        else
        {
            livesText.text = "Lives:" + lives.ToString();
        }
    }

    public void incrementMoney(int value)
    {
        money += value;
        moneyText.text = "Cash:" + money.ToString();
    }

    public void decrementMoney(int value)
    {
        money -= value;
        moneyText.text = "Cash:" + money.ToString();
    }

    public void incrementWaveCounter()
    {
        wave++;
        waveText.text = "Wave:" + wave.ToString();
    }

    //This function tests if the player can afford an object.
    //It assumes the player will purchase it so we subtract the cost here as well.
    public bool canAffordPurchase(int cost)
    {
        if (money - cost >= 0)
        {
            decrementMoney(cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    void gameOver()
    {
        if(canLose == true)
        {
            analyticsManager.sendLoseEvent();
            Application.Quit();
            //EditorApplication.isPlaying = false;        
        }
        else
        {
            return;
        }
    }

    public void setIsBuildMenuOpen(bool value)
    {
        bIsBuildMenuOpen = value;
    }

    public bool getIsBuildMenuOpen()
    {
        return bIsBuildMenuOpen;
    }

}
