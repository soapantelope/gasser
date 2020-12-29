using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Wander Range")]
    public float leftPoint;
    public float rightPoint;
    int currentDirection = 1;

    [Header("Ranges")]
    public float sightRange;
    public float attackStopRange;
    public float safeStopRange;

    [Header("Speeds")]
    public float wanderSpeed;
    public float chaseSpeed;
    public float escapeSpeed;

    [Header("Behavior")]
    public bool agressive = false;
    public int status = 0;
    private List<string> statuses = new List<string>() {"wander", "escape", "chase"};

    [Header("References")]
    public LayerMask playerLayer;
    public GameObject player;

    void Start()
    {
        // stopRange = enemyFight.stopRange;
    }

    void Update()
    {
        if (agressive) {
            checkForPlayer();
        }

        // Calls the method from the current status
        Invoke(statuses[status], 0f);
    }

    void checkForPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, attackStopRange, playerLayer)) status = 0;
        else if (Physics2D.OverlapCircle(transform.position, sightRange, playerLayer)) status = 2;
        else status = 0;
    }

    private void wander()
    {
        if (transform.position.x > rightPoint || transform.position.x < leftPoint) {
            currentDirection *= -1;
        }
        transform.position += new Vector3(currentDirection, 0, 0) * Time.deltaTime * wanderSpeed;
    }

    private void escape() {
        transform.position += new Vector3(-directionToPlayer(), 0, 0) * Time.deltaTime * escapeSpeed;
        if (!Physics2D.OverlapCircle(transform.position, safeStopRange, playerLayer)) status = 0;
    }

    private void chase() {
        transform.position += new Vector3(directionToPlayer(), 0, 0) * Time.deltaTime * chaseSpeed;
    }

    private int directionToPlayer() {
        float offset = (player.transform.position.x - transform.position.x);
        return (int)(offset / Mathf.Abs(offset));
    }

    // In editor:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector3(leftPoint, transform.position.y + 0.25f, 0), new Vector3(rightPoint, transform.position.y + 0.25f, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, safeStopRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackStopRange);
    }
}
