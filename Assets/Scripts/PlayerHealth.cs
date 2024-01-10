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


    public static event Action<float> OnPlayerDamageTaken;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        if (healthAmount <= 0)
        {
            // death
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

    public void CheckHealth()
    {
        timer += Time.deltaTime;
        
        if (timer > recordTime)
        {
            damageTaken = playerPrevHealth - healthAmount;
            Debug.Log("damage statistics: " + damageTaken + "damage taked during " + recordTime);

			if (damageTaken < 40) 
			{
                OnPlayerDamageTaken?.Invoke(damageTaken);
			}

			timer = 0;
			damageTaken = 0;
			playerPrevHealth = healthAmount;
        }
        
    }
}
