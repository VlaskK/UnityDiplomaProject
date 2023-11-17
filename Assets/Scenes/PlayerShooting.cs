using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform FirePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    private WeaponHold link;

    private void Start()
    {
        link = GetComponent<WeaponHold>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && link.hold) Shoot();
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}