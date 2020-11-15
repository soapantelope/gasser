using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform tsfm;
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 0f;
    public float jumpForce = 0f;

    public bool isGrounded = false;
    private int direction = 1;
    private float X = 0f;
    private float previousX = 0f;
   
    void Start()
    {
        tsfm = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        move();
        jump();
    }

    void move() {
        X = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(X, 0f, 0f);
        
        // Make sprite face the direction it's moving
        if (X < 0 && previousX >= 0) direction = -1;
        else if (X > 0 && previousX <= 0) direction = 1;

        tsfm.localScale = new Vector3(System.Math.Abs(tsfm.localScale.x) * direction, tsfm.localScale.y, 1);
        tsfm.position += movement * Time.deltaTime * moveSpeed;
        previousX = X;

        animate();
    }

    void jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(new Vector3(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void animate() {
        if (X == 0f) animator.SetBool("isWalking", false);
        else animator.SetBool("isWalking", true);
    }
}
