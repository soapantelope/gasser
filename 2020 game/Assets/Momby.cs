using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momby : MonoBehaviour
{
    [Header("Stats")]
    public float sightRange;
    public float wallDistance;
    public float speed;
    public float cooldown;
    public float damage;
    public float knockback;

    [Header("References")]
    public Player player;
    public GameObject door;
    public EnemyHealthBar healthBar;
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    private bool seen = false;
    public bool hitWall = false;
    private int direction;
    private float nextAttackTime;

    void Update()
    {
        if (!seen && Physics2D.OverlapCircle(transform.position, sightRange, playerLayer))
        {
            gameObject.GetComponent<DialogueTrigger>().triggerDialogue();
            player.currentlyTalking = true;
            player.npc = gameObject;
            door.SetActive(true);
            healthBar.gameObject.SetActive(true);
            seen = true;
            gameObject.layer = 8;
        }

        else if (seen && !player.currentlyTalking) {
            fight();
        }
    }

    private void fight() {
        if (Time.time >= nextAttackTime) {
            hitWall = false;
            direction = directionToPlayer();
            transform.localScale = new Vector3(System.Math.Abs(transform.localScale.x) * direction, transform.localScale.y, 1);
            StartCoroutine(charge(direction));
            nextAttackTime = Time.time + 10000;
        }
    }

    IEnumerator charge(int direction) {
        while (!hitWall) {
            transform.position = transform.position + new Vector3(speed * direction * Time.deltaTime, 0, 0);
            hitWall = Physics2D.Raycast(transform.position + new Vector3(wallDistance, 0, 0), Vector2.left, wallDistance * 2, groundLayer);
            yield return null;
        }
        nextAttackTime = Time.time + cooldown;
    }

    private int directionToPlayer()
    {
        float offset = (player.transform.position.x - transform.position.x);
        return (int)(offset / Mathf.Abs(offset));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - new Vector3(wallDistance, 0, 0),
            transform.position + new Vector3(wallDistance, 0, 0));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && collision.GetContact(0).point.y <= gameObject.GetComponent<SpriteRenderer>().bounds.max.y - 0.5f &&
                (direction == -1 && collision.GetContact(0).point.x <= transform.position.x ||
                direction == 1 && collision.GetContact(0).point.x >= transform.position.x))
        {
            player.takeDamage(damage * Time.deltaTime);
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback/2, knockback) * direction, ForceMode2D.Impulse);
        }
    }
}
