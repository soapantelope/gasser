using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        // play hurt animation

        if (health <= 0) {
            die();
        }
    }

    public void die() {
        Debug.Log("You died");
    }
}
