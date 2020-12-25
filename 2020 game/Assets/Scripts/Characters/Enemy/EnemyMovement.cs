using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemy
{
    public float leftPoint;
    public float rightPoint;
    int currentDirection = 1;

    public float sightRange;
    public float stopRange;

    public float speed;
    public float damage;
    public bool agressive = false;

    public int status = 0;
    private List<string> statuses = new List<string>() {"wander", "escape", "chase"};

    public LayerMask playerLayer;

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
        Physics2D.OverlapCircleAll(transform.position, sightRange, playerLayer);
        status = 2;
    }

    void wander()
    {
        if (transform.position.x > rightPoint || transform.position.x < leftPoint) {
            currentDirection *= -1;
        }
        transform.position += new Vector3(currentDirection, 0, 0) * Time.deltaTime * speed;
    }

    void escape() {
        Debug.Log("Escape");
    }

    void chase() {
        Debug.Log("Chase");
    }

    // In editor:
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(new Vector3(leftPoint, transform.position.y, 0), new Vector3(rightPoint, transform.position.y, 0));
    }
}
