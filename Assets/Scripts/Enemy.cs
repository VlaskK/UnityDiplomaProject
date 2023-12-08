using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;

    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (distance < 10)
        {
            this.Chase(angle);
        }
        
        
    }

    private void Chase(float angle)
    {
        transform.position = Vector2.MoveTowards(
            this.transform.position,
            player.transform.position,
            speed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
    
}