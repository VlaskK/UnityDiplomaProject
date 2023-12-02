using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public float enemyRange = 10;
    public float moveSpeed = 1;
    public float moveDistance = 5;
    
    private float chaseDistance = 10;

    private GameObject player;
    private float timer;
    private bool movingRight = true;
    private float initialPositionX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < enemyRange)
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

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    void movement()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime));
            if (transform.position.x > (initialPositionX + moveDistance))
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
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
            moveSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }


}
