using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : DifficultySettings
{
    public Image HealthBar;

    public float healthAmount = PlayerStartingHealth;
    private float playerPrevHealth = PlayerStartingHealth;

    private bool isEnemyEngaged = false;
    private int activeEnemies = 0;
    private int killedEnemies = 0;
    private float healthBeforeBattle = PlayerStartingHealth;

    public static event Action<float> OnPlayerDamageTaken;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            // death
        }
        
        if (activeEnemies == 0 && isEnemyEngaged)
        {
            Debug.Log("Здоровье потрачено за бой: " + (healthBeforeBattle - healthAmount));
            OnPlayerDamageTaken.Invoke(healthBeforeBattle - healthAmount);
            isEnemyEngaged = false;
        }
    }
    
    private void OnEnable()
    {
        EnemyFollowShoot.OnEnemyEngaged += HandleEnemyEngaged;
        EnemyHealth.OnEnemyEngaged += HandleEnemyEngaged;
    }

    private void OnDisable()
    {
        EnemyFollowShoot.OnEnemyEngaged -= HandleEnemyEngaged;
        EnemyHealth.OnEnemyEngaged -= HandleEnemyEngaged;
    }

    private void HandleEnemyEngaged(bool enemyEngaged)
    {
        if (enemyEngaged)
        {
            isEnemyEngaged = true;
            activeEnemies++;
            healthBeforeBattle = healthAmount;
        }
        else
        {
            activeEnemies--;
            killedEnemies++;
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        HealthBar.fillAmount = healthAmount / 100f;
        
        gameObject.GetComponent<Animator>().SetTrigger("Damage");
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healingAmount, 0, 100);
        
        HealthBar.fillAmount = healthAmount / 100f;
    }
}
