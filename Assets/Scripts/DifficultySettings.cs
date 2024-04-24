﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DifficultySettings : MonoBehaviour
{
    //GameScore Variables
    protected static int[] baseScoreValue = { 5, 7, 10, 15, 20, 25, 30, 37, 45, 55, 0, 5 }; //score value for hitting enemy once
    
    public static int coinsWinCondition = 5;
    public static int fragsWinCondition = 5;

    //Player Variables
    protected static float PlayerMoveSpeed = 8;
    protected static float PlayerStartingHealth = 100f;

    //Enemy Variables
    protected static int EnemyAttackDamage = 2;
    protected static float EnemyStartHealth = 10;
    protected static float EnemyRange = 10f;
    protected static float EnemySpeed = 1;


    public float recordTime = 5f;
    public float timer = 0f;
    public float damageTaken = 0;
    
    // Tanku Active PlayStyles
    public float maxHealthValue = 100;
    public float maxSpeedValue = 2;
    public float minHealthValue = 10;
    public float minSpeedValue = 1;
    public int multiplier = 2;

    public static void changeEnemyStats(float speed, float health)
    {
        EnemySpeed *= 1 + speed;
        EnemyStartHealth *= 1 + health;
        
        Debug.Log("Enemy speed: " + EnemySpeed);
        Debug.Log("Enemy health: " + EnemyStartHealth);
    }

    // Reset Difficulty to default, you can call this when game resets or player respawns
    public static void ResetEnemyStats()
    {
        EnemyAttackDamage = 2; // Reset to original value, you can also store these defaults somewhere
        EnemyStartHealth = 10;
        EnemyRange = 10f;
    }
    
    
}