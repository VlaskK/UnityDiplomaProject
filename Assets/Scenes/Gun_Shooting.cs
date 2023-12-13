using UnityEngine;

public class Gun_Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public bool Active = false;
    public int numBullet = 30;
    public float bulletForce = 20f;
    
    private WeaponHold link;

    private void Start()
    {
        link = GetComponent<WeaponHold>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log($"Active.Shoot Gun1 = {Active} -- GameObj = {gameObject.name}");
        if (Input.GetButtonDown("Fire1") && Active && numBullet > 0)
        {
            Shoot();
            numBullet--;
        }
        
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}