using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int money = 0;
    int lives = 2;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;

    private void Start()
    {
        moneyText.text = money.ToString();
        livesText.text = lives.ToString();
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

    public void incrementMoney(int score)
    {
        money += score;
        moneyText.text = money.ToString();
    }

    public void decrementMoney(int score)
    {
        money -= score;
        moneyText.text = money.ToString();
    }

    void gameOver()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        
    }

}
