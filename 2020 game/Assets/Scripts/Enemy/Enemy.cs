using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool alive = true;
    public bool hurt = false;

    public float maxHealth;
    public float health;

    public float thirstGift;

    public Animator animator;

    public Collider2D myCol;

    void Start()
    {
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
        if (gameObject.GetComponent<Momby>() != null) {
            gameObject.GetComponent<Momby>().die();
        }
        alive = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

}
