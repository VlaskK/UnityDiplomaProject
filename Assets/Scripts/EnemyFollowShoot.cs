using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowShoot : DifficultySettings
{
    public GameObject bullet;
    public Transform bulletPos;
    public float moveDistance = 5;
    
    private float chaseDistance = 10;

    private GameObject player;
    private bool movingRight = true;
    private float initialPositionX;
    
    // for tanky playstyle 
    private float playerPrevDamageTaken = 0;
    private float enemyPrevDamageTaken = 0;
    
    public static event Action<bool> OnEnemyEngaged;
    private bool eventTriggered = false;

    private float playerDamageDuringFight = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < EnemyRange)
        {
            if (!eventTriggered)
            {
                eventTriggered = true;
                OnEnemyEngaged.Invoke(true);
            }
            attackPlayer();
        }
        else
        {
            if (eventTriggered)
            {
                eventTriggered = false;
                OnEnemyEngaged.Invoke(false);
            }
            
            movement();
        }
    }
    
    


	private void OnEnable() {
        PlayerMovement.OnRotationStatistic += HandlePlayerRotation;
        PlayerHealth.OnPlayerDamageTaken += HandleDamageStatistics;
    }

    private void OnDisable() {
        PlayerMovement.OnRotationStatistic -= HandlePlayerRotation;
        PlayerHealth.OnPlayerDamageTaken -= HandleDamageStatistics;
    }


    private void HandlePlayerRotation(int rotationCount, int enemies, float fightTime)
    {
        var littleRotation = rotationCount < 15; // увеличиваем противодействия стратегии танк
        var muchRotation = rotationCount > 50; // увеличиваем противодействия стратегии мобильность

        var hardCondition = playerDamageDuringFight > PlayerStartingHealth * 0.5;
        var easyCondition = playerDamageDuringFight < PlayerStartingHealth * 0.2;
        
        var muchEnemies = enemies > 5; // увеличиваем статы

        float healthMultiplier = 10;
        float speedMultiplier = 2;

        if (hardCondition)
        {
            multiplier = fightTime < 10 ? multiplier / 4 : multiplier / 2;
        }

        if (easyCondition)
        {
            multiplier /= 2;
            multiplier = (fightTime < 10) && muchEnemies ? multiplier * 4 : multiplier * 2;
        }

        if (littleRotation) // Tanky Playstyle
        {
            healthMultiplier = healthMultiplier > maxHealthMultiplier ? 0 : healthMultiplier * multiplier;
            speedMultiplier = muchEnemies && speedMultiplier > minSpeedMultiplier ? speedMultiplier / multiplier : 0;
        }

        if (muchRotation) // Active playstyle
        {
            speedMultiplier = speedMultiplier > maxSpeedMultiplier ? 0 : speedMultiplier * multiplier;
            healthMultiplier = muchEnemies && healthMultiplier > minHealthMultiplier ? healthMultiplier/ multiplier : 0;
        }


        changeEnemyStats(speedMultiplier / 100, healthMultiplier / 100);
    }

    private void HandleDamageStatistics(float damageTaken)
    {
        playerDamageDuringFight = damageTaken;
    }

    private void attackPlayer()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            shoot();
        }
            
        chaseDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Chase(angle);
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    void movement()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * (EnemySpeed * Time.deltaTime));
            if (transform.position.x > (initialPositionX + moveDistance))
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * (EnemySpeed * Time.deltaTime));
            if (transform.position.x < (initialPositionX - moveDistance))
            {
                movingRight = true;
            }
        }
    }
    
    private void Chase(float angle)
    {
        transform.position = Vector2.MoveTowards(
            this.transform.position,
            player.transform.position,
            EnemySpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
