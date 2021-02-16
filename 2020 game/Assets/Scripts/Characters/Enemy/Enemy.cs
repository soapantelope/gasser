using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool alive = true;
    public bool hurt = false;

    public float maxHealth = 100f;
    public float health;

    public float thirstGift;

    public Animator animator;

    public Collider2D myCol;

    void Start()
    {
        health = maxHealth;
        animator = gameObject.GetComponent<Animator>();
        myCol = gameObject.GetComponent<Collider2D>();
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        hurt = true;

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
