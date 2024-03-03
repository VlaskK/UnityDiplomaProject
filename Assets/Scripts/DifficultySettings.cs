using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DifficultySettings : MonoBehaviour
{
    //GameScore Variables
    protected static int[] baseScoreValue = { 5, 7, 10, 15, 20, 25, 30, 37, 45, 55, 0, 5 }; //score value for hitting enemy once

    protected static int coinsWinCondition = 1;
    protected static int fragsWinCondition = 10;
    
    //Player Variables
    protected static float PlayerMoveSpeed = 8;
    protected static float PlayerStartingHealth = 100f;

    //Enemy Variables
    protected static int EnemyAttackDamage = 2;
    protected static int EnemyStartHealth = 10;
    protected static float EnemyRange = 10f;
    protected static float EnemySpeed = 1;


    public float recordTime = 5f;
    public float timer = 0f;
    public float damageTaken = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void ChangeEnemyStats(float difficultyMultiplier)
    {
        //TODO make a correct multiplier 
        Debug.Log("Increase enemy stats by" + difficultyMultiplier);
        EnemyAttackDamage = Mathf.RoundToInt(EnemyAttackDamage * difficultyMultiplier);
        EnemyStartHealth = Mathf.RoundToInt(EnemyStartHealth * difficultyMultiplier);
        EnemySpeed *= difficultyMultiplier;
        EnemyRange *= difficultyMultiplier;
    }

    public static void ChangeEnemyDifficulty(int level)
    {
        switch (level)
        {
            case 1:
                Debug.Log("diffuclty 1");
                EnemySpeed = 1;
                EnemyRange = 10f;
                break;
            case 2:
                Debug.Log("diffuclty 2") ;
                EnemySpeed = 1.5f;
                EnemyRange = 15f;
                break;
            case 3:
                Debug.Log("diffuclty 3");
                EnemySpeed = 2;
                EnemyRange = 20f;
                break;
        }
    }

    // Reset Difficulty to default, you can call this when game resets or player respawns
    public static void ResetEnemyStats()
    {
        EnemyAttackDamage = 2; // Reset to original value, you can also store these defaults somewhere
        EnemyStartHealth = 10;
        EnemyRange = 10f;
    }
    
    public void EndLevel()
    {
        
    }
    
}