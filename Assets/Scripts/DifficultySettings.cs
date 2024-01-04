using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultySettings : MonoBehaviour {

    //GameScore Variables
    protected static int[] baseScoreValue = {5,7,10,15,20,25,30,37,45,55,0,5};            //score value for hitting enemy once
    
    //Player Variables
    protected static float PlayerMoveSpeed = 8;
    protected static float PlayerStartingHealth = 100f;

    //Enemy Variables
    protected static int EnemyAttackDamage = 2;
    protected static int EnemyStartHealth = 10;
    protected static float EnemyRange = 10f;
    
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static void IncreaseEnemyStats(float difficultyMultiplier) {
		EnemyAttackDamage = Mathf.RoundToInt(EnemyAttackDamage * difficultyMultiplier);
		EnemyStartHealth = Mathf.RoundToInt(EnemyStartHealth * difficultyMultiplier);
		EnemyRange *= difficultyMultiplier;
	}

	// Reset Difficulty to default, you can call this when game resets or player respawns
	public static void ResetEnemyStats() {
		EnemyAttackDamage = 2; // Reset to original value, you can also store these defaults somewhere
		EnemyStartHealth = 10;
		EnemyRange = 10f;
	}

}
