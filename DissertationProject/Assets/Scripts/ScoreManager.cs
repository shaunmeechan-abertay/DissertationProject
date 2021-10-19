using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int money = 10;
    int lives = 2;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    AnalyticsManager analyticsManager;
    private void Start()
    {
        moneyText.text = money.ToString();
        livesText.text = lives.ToString();
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
            livesText.text = lives.ToString();
        }
    }

    public void incrementMoney(int value)
    {
        money += value;
        moneyText.text = money.ToString();
    }

    public void decrementMoney(int value)
    {
        money -= value;
        moneyText.text = money.ToString();
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
        analyticsManager.sendLoseEvent();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        
    }

}
