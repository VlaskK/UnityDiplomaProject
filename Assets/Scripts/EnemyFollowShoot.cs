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
        else
        {
            movement();
        }
    }


	private void OnEnable() {
        PlayerMovement.OnRotationStatistic += HandlePlayerRotation;
    }

    private void OnDisable() {
        PlayerMovement.OnRotationStatistic -= HandlePlayerRotation;
    }


    private void HandlePlayerRotation(int rotationCount)
    {
        if (rotationCount > 200)
        {
            ChangeEnemyDifficulty(3);
            return;
        } else if (rotationCount > 150)
        {
            ChangeEnemyDifficulty(2);
            return;
        }
        
        ChangeEnemyDifficulty(1);

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
