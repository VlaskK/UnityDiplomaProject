using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider enemySlider;

    public void SetEnemyCount()
    {
        int value = (int)enemySlider.value;
        DifficultySettings.fragsWinCondition = value;
    }
    
    public void SetCoinsCount()
    {
        int value = (int)enemySlider.value;
        DifficultySettings.coinsWinCondition = value;
    }
}
