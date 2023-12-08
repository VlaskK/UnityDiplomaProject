using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float healthAmount = 50f;
    public Rigidbody2D rb;

    private float timer;
    public Sprite mob1_damage;
    private Sprite mob1;
    public Sprite mob2_damage;
    private Sprite mob2;
    
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
        if (gameObject.tag == "Enemy1")
        {
            mob1 = gameObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = mob1_damage;
            
            gameObject.GetComponent<SpriteRenderer>().sprite = mob1;
        }
        if (gameObject.tag == "Enemy2")
        {
            mob2 = gameObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = mob2_damage;
            gameObject.GetComponent<SpriteRenderer>().sprite = mob2;
        }
    }
}
