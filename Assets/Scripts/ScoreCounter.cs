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

    private void Awake()
    {
        instance = this;
        start = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "COINS: " + currentCoins.ToString();
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
        coinText.text = "COINS: " + currentCoins.ToString();


        if (currentCoins > coinsWinCondition)
        {
            currentCoins = 0;
            coinText.text = "COINS: " + currentCoins.ToString();
            OnEndLevel.Invoke(time, coinsWinCondition, fragPoints);
            stopTimer();
            start = true;
        }
    }

    public void increaseFrag(int v)
    {
        fragPoints += v;
        fragText.text = "FRAGS: " + fragPoints.ToString();


        if (fragPoints > fragsWinCondition)
        {
            fragPoints = 0;
            fragText.text = "FRAGS: " + fragPoints.ToString();
            OnEndLevel.Invoke(time, currentCoins, fragsWinCondition);
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