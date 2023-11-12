using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    private float timer; 
    // Update is called once per frame
    private void Start()
    {
        Destroy(gameObject, 4);
    }
    
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
        Debug.Log("enemy touched");
        Debug.Log(col);
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("enemy hit");
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(20);
            Destroy(gameObject);
        }
    }
}