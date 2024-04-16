using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject player;
    public float force;
    private Rigidbody2D rb;

    private float timer; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
            Destroy(gameObject);
        }
        
        // if (col.gameObject.CompareTag("Enemy1"))
        // {
        //     col.gameObject.GetComponent<EnemyHealth>().TakeDamage(20);
        //     Destroy(gameObject);
        // }
        //
        // if (col.gameObject.CompareTag("Enemy2"))
        // {
        //     col.gameObject.GetComponent<EnemyHealth>().TakeDamage(20);
        //     Destroy(gameObject);
        // }
        
        
        if (col.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    
}
