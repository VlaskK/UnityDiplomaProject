using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class DifficultySettings : MonoBehaviour
{
    //GameScore Variables
    protected static int[] baseScoreValue = { 5, 7, 10, 15, 20, 25, 30, 37, 45, 55, 0, 5 }; //score value for hitting enemy once
    
    public static int coinsWinCondition = 3;
    public static int fragsWinCondition = 5;
    
    public static int currentCoins = 0;
    public static int fragPoints = 0;

    //Player Variables
    protected static float PlayerMoveSpeed = 8;
    protected static float PlayerStartingHealth = 100f;

    //Enemy Variables
    protected static int EnemyAttackDamage = 2;
    protected static float EnemyStartHealth = 50;
    protected static float EnemyRange = 10f;
    protected static float EnemySpeed = 1;

    public static float playerDamageTaken = 0;

    public float recordTime = 5f;
    public float timer = 0f;
    public float damageTaken = 0;
    
    // Tanku Active PlayStyles
    public float maxHealthMultiplier = 50;
    public float maxSpeedMultiplier = 5;
    public float minHealthMultiplier = 10;
    public float minSpeedMultiplier = 1;
    public int multiplier = 2;

    private static float maxEnemySpeed = 3;
    private static float maxEnemyHealth = 150;
    
    public static void changeEnemyStats(float speed, float health)
    {
        if (EnemySpeed <= maxEnemySpeed)
        {
            EnemySpeed *= 1 + speed;
        }

        if (EnemyStartHealth <= maxEnemyHealth)
        {
            EnemyStartHealth *= 1 + health;
        }


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