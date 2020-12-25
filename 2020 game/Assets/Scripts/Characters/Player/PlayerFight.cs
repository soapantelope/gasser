using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : Player
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public float attackRate;
    private float nextAttackTime;
    public float damage;

    public LayerMask enemyLayer;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Attack");
                attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void attack()
    {
        animator.SetTrigger("meleeFight");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in enemies) {
            enemy.GetComponent<Enemy>().takeDamage(damage);
        }
    }

    // In editor:
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
