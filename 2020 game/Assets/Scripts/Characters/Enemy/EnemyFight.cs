using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public float attackRange;
    public float attackRate;
    private float nextAttackTime;
    public float damage;

    public LayerMask playerLayer;
    public Player player;
    public EnemyMovement movement;

    private void Start()
    {
        movement = gameObject.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.agressive && Physics2D.OverlapCircle(transform.position, attackRange, playerLayer) && Time.time > nextAttackTime) {
            attack();
            nextAttackTime = Time.time + 1f / attackRate;
        } 
    }

    void attack() {
        player.takeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
