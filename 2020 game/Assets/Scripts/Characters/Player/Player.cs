using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;

    public float maxThirst = 100f;
    public float thirst;
    public float thirstRate;

    public GameObject entrance;
    public GameObject exit;

    void Start()
    {
        if (Static.enterExit == "Exit") transform.position = entrance.transform.position;
        else transform.position = exit.transform.position;

        health = maxHealth;
        thirst = maxThirst;
    }

    void Update()
    {
        thirst -= thirstRate;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
