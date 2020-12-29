using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Collider2D col;
    public PlayerFight player;
    public LayerMask enemyLayer;

    private int damage = 0;
    public float bulletRange;

    private void Start()
    {
        gameObject.SetActive(false);
        col = gameObject.GetComponent<Collider2D>();
    }

    public IEnumerator shoot(int currentDamage, float speed, Vector3 start, float range, int direction, float knockback) {
        gameObject.SetActive(true);
        transform.position = start;
        damage = currentDamage;
        if (direction > 0)
        {
            while (transform.position.x <= start.x + range)
            {
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
                if (Physics2D.OverlapCircle(transform.position, bulletRange, enemyLayer))
                {
                    player.damage(currentDamage, transform.position, range, knockback);
                    gameObject.SetActive(false);
                    break;
                }
                yield return null;
            }
        }
        else
        {
            while (transform.position.x >= start.x - range)
            {
                transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * speed;
                if (Physics2D.OverlapCircle(transform.position, bulletRange, enemyLayer))
                {
                    player.damage(currentDamage, transform.position, range, knockback);
                    gameObject.SetActive(false);
                    break;
                }
                yield return null;
            }
        }
    }

    // In editor:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bulletRange);
    }
}
