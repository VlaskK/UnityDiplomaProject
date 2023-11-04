using System;
using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform FirePoint;
    public GameObject bulletPrefab;
    // private bool hold = true; // здесь я хочу проверку сделать на подобранное оружие, чтобы без него он не стрелял
    WeaponHold link;
        
    void Start()
    {
        link = GetComponent<WeaponHold>();
        
    }

    public float bulletForce = 20f;

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