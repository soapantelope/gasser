using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uses a short raycast to check if the ground is close enough to jump
public class GroundCheck : MonoBehaviour
{
    public Collider2D col;
    private GameObject player;
    private float distToGround;

    void Start()
    {
        player = gameObject.transform.parent.gameObject;
        col = gameObject.GetComponent<Collider2D>();
        distToGround = col.bounds.extents.y;
    }

    void Update()
    {
        player.GetComponent<PlayerMovement>().isGrounded = isGrounded();
    }

    bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, -transform.up, distToGround + 0.5f);
    }

}
