using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float healthAmount = 50f;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        
        gameObject.GetComponent<Animator>().SetTrigger("Damage");
    }
}
