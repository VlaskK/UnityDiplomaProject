using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;

    public TMP_Text coinText;
    public TMP_Text fragText;

    public int currentCoins = 0;
    public int fragPoints = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "COINS: " + currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = "COINS: " + currentCoins.ToString();
    }

    public void increaseFrag(int v)
    {
        fragPoints += v;
        fragText.text = "FRAGS: " + fragPoints.ToString();
    }

}