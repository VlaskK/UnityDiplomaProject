using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    private float timeAttack;

    public float startTimeAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public float damage;
    private Animator _anim;
    private WeaponHold _weaponHold;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _weaponHold = GetComponent<WeaponHold>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAttack <= 0)
        {
            if (Input.GetButton("Fire1") && !_weaponHold.hold)
            {
                _anim.SetTrigger("Attack");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }

            timeAttack = startTimeAttack;
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
