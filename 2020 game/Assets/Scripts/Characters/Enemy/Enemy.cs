using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool alive = true;

    public float maxHealth = 100f;
    public float health;

    public EnemyMovement controller;
    public Animator animator;

    public Collider2D myCol;
    public Collider2D projectile;

    void Start()
    {
        health = maxHealth;
        controller = gameObject.GetComponent<EnemyMovement>();
        animator = gameObject.GetComponent<Animator>();
        myCol = gameObject.GetComponent<Collider2D>();
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        controller.status = 1;

        animator.SetTrigger("Hurt");

        if (health <= 0f)
        {
            die();
        }        
    }

    public void die()
    {
        alive = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

}
