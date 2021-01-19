using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float health;
    public float maxThirst = 100f;
    public float thirst;
    public float thirstRate;

    [Header("NPC Interaction")]
    public LayerMask NPCLayer;
    public float talkRadius;

    [Header("Navigation")]
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

        if (Input.GetKeyDown(KeyCode.Q) && Physics2D.OverlapCircle(transform.position, talkRadius, NPCLayer)) {
            foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, talkRadius, NPCLayer)) {
                col.gameObject.GetComponent<DialogueTrigger>().triggerDialogue();
            }
        }
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
