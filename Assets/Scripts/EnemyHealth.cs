using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyHealth : DifficultySettings
{
    
    public float healthAmount = EnemyStartHealth;
    public Rigidbody2D rb;
    
    private float enemyPrevHealth = EnemyStartHealth;
    public static event Action<float> OnEnemyDamageTaken;
    public static event Action<bool> OnEnemyEngaged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDamage();
        if (healthAmount <= 0)
        {
            OnEnemyEngaged.Invoke(false);
            Destroy(gameObject);
			ScoreCounter.instance.increaseFrag(1);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        
        gameObject.GetComponent<Animator>().SetTrigger("Damage");
    }
    
    
    public void CheckDamage()
    {
        timer += Time.deltaTime;
        
        if (timer > recordTime)
        {
            damageTaken = enemyPrevHealth - healthAmount;
            // Debug.Log("damage statistics: " + damageTaken + "damage taked during " + recordTime);

            if (damageTaken < 40) 
            {
                OnEnemyDamageTaken?.Invoke(damageTaken);
            }

            timer = 0;
            damageTaken = 0;
            enemyPrevHealth = healthAmount;
        }
        
    }
}
