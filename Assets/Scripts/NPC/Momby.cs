using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Momby : MonoBehaviour
{
    [Header("Stats")]
    public float sightRange;
    public float wallDistance;
    public float speed;
    public float cooldown;
    public float damage;
    public float knockback;
    public float explodePoint;
    public float minSpeedToDamage;
    public float collisionDamageMultiplier;

    [Header("References")]
    public Player player;
    public GameObject door;
    public EnemyHealthBar healthBar;
    public GameObject outsideBimby;
    public SFXManager sfx;
    public GameObject transformText;
    public SceneFade black;
    public Text TBC;

    // Make this more flexible later
    public SceneTrigger explodeTrigger;
    public GameObject outsideDoor;
    public GameObject outsideBlocker;
    public GameObject outsideHouse;
    public GameObject musicTrigger;
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    private bool seen = false;
    private int direction;
    private Enemy body;
    private Rigidbody2D rb;

    private bool exploded = false;

    private void Start()
    {
        body = gameObject.GetComponent<Enemy>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (body.alive)
        {
            if (!seen && Physics2D.OverlapCircle(transform.position, sightRange, playerLayer))
            {
                sfx.stop("house");
                sfx.play("action");
                gameObject.GetComponent<DialogueTrigger>().triggerDialogue();
                player.currentlyTalking = true;
                player.npc = gameObject;
                door.SetActive(true);
                healthBar.gameObject.SetActive(true);
                seen = true;
                gameObject.layer = 8;
            }

            if (!exploded && body.health <= explodePoint)
            {
                exploded = true;
                explodeStage();
            }
        }
    }

    public void startAttacking() {
        direction = directionToPlayer();
        transform.localScale = new Vector3(System.Math.Abs(transform.localScale.x) * -direction, transform.localScale.y, 1);
        StartCoroutine(charge(direction));
    }

    IEnumerator charge(int direction) {
        yield return new WaitForSeconds(cooldown);
        bool hitWall = false;
        while (!hitWall)
        {
            transform.position = transform.position + new Vector3(speed * direction * Time.deltaTime, 0, 0);
            hitWall = Physics2D.Raycast(transform.position, new Vector2(direction, 0), wallDistance, groundLayer);
            yield return null;
        }        
        startAttacking();
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
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(wallDistance, 0, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag == "Ground" && collision.relativeVelocity.magnitude > minSpeedToDamage) {
            body.takeDamage(collision.relativeVelocity.magnitude * collisionDamageMultiplier);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (body.alive && collision.gameObject.layer == 9 && collision.GetContact(0).point.y <= gameObject.GetComponent<SpriteRenderer>().bounds.max.y - 0.5f &&
                (direction == -1 && collision.GetContact(0).point.x <= transform.position.x ||
                direction == 1 && collision.GetContact(0).point.x >= transform.position.x))
        {
            player.takeDamage(damage * Time.deltaTime);
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback/2, knockback) * direction, ForceMode2D.Impulse);
        }
    }

    private void explodeStage() {
        // animate explosion
        sfx.play("crash");
        player.canTransform = true;
        transformText.gameObject.SetActive(true);
        explodeTrigger.gameObject.SetActive(true);
        transform.parent = outsideDoor.transform.parent.transform;
        outsideBimby.gameObject.SetActive(true);
        transform.SetAsFirstSibling();
        musicTrigger.SetActive(false);
        outsideHouse.SetActive(false);
        outsideDoor.SetActive(false);
        outsideBlocker.SetActive(true);
    }

    public void die() {
        transformText.gameObject.SetActive(false);
        musicTrigger.SetActive(true);
        healthBar.gameObject.SetActive(false);
        outsideBlocker.SetActive(false);
        StartCoroutine(finishGame());
    }

    IEnumerator finishGame() {
        for (float i = 0; i <= 1; i += 0.3f * Time.deltaTime)
        {
            black.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
        TBC.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }
}
