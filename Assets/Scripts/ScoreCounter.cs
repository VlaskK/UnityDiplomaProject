using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : DifficultySettings
{
    public static ScoreCounter instance;

    public TMP_Text coinText;
    public TMP_Text fragText;

    public int currentCoins = 0;
    public int fragPoints = 0;
    
    public float time = 0;
    public bool start = false;
    
    
    public static event Action<float, int, int> OnEndLevel;
    
    private string coinsScore => "COINS: " + currentCoins.ToString() + "/" + (coinsWinCondition + 1);
    private string enemyScore => "FRAGS: " + fragPoints.ToString() + "/" + (fragsWinCondition + 1);
    

    private void Awake()
    {
        instance = this;
        start = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = coinsScore;
        fragText.text = enemyScore;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start) 
        {
            time += Time.deltaTime;
        }
    }

    public void increaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = coinsScore;


        if (currentCoins > coinsWinCondition)
        {
            currentCoins = 0;
            coinText.text = coinsScore;
            var fragValue = fragPoints;
            fragPoints = 0;
            fragText.text = enemyScore;
            OnEndLevel.Invoke(time, coinsWinCondition, fragValue);
            stopTimer();
            start = true;
        }
    }

    public void increaseFrag(int v)
    {
        fragPoints += v;
        fragText.text = enemyScore;


        if (fragPoints > fragsWinCondition)
        {
            fragPoints = 0;
            fragText.text = enemyScore;
            var coinsValue = currentCoins;
            currentCoins = 0;
            coinText.text = coinsScore;
            OnEndLevel.Invoke(time, coinsValue, fragsWinCondition);
            stopTimer();
            start = true;
        }
    }

    public void stopTimer()
    {
        Debug.Log("Время " + time);
        start = false;
        time = 0;
    }
}